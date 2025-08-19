using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BasincIzlemeProjesi.Controllers
{
    [ApiController]
    [Route("api/veriler")]
    public class VerilerController : ControllerBase
    {
        private readonly GirisSistemiContext _context;

        public VerilerController(GirisSistemiContext context)
        {
            _context = context;
        }

        [HttpGet("veriler")]
        public IActionResult GetVeriler(string tip = "son10", int sayfa = 1)
        {
            const int sayfaBoyutu = 10;
            var sorgu = _context.Veriler.AsQueryable();

            switch (tip.ToLower())
            {
                case "son10":
                    sorgu = sorgu.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman);
                    break;

                case "son10dakika":
                    sorgu = sorgu.Where(v => v.Zaman >= DateTime.Now.AddMinutes(-10))
                                 .OrderBy(v => v.Zaman);
                    break;

                case "son1saat":
                    sorgu = sorgu.Where(v => v.Zaman >= DateTime.Now.AddHours(-1))
                                 .OrderBy(v => v.Zaman);
                    break;

                case "tumveriler":
                    sorgu = sorgu.OrderBy(v => v.Zaman);
                    break;

                default:
                    sorgu = sorgu.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman);
                    break;
            }

            var toplamVeri = sorgu.Count();
            var toplamSayfa = (int)Math.Ceiling(toplamVeri / (double)sayfaBoyutu);

            var veriler = sorgu.Skip((sayfa - 1) * sayfaBoyutu)
                               .Take(sayfaBoyutu)
                               .Select(v => new
                               {
                                   v.Id,
                                   v.CihazId,
                                   v.Deger,
                                   Zaman = DateTime.SpecifyKind(v.Zaman, DateTimeKind.Unspecified)
                                                  .ToString("yyyy-MM-dd HH:mm:ss")
                               }).ToList();

            return Ok(new
            {
                veriler,
                toplamSayfa,
                sayfa
            });
        }

        [HttpPost("veri-ekle")]
        public async Task<IActionResult> VeriEkle([FromBody] Veri veri)
        {
            if (veri == null || string.IsNullOrEmpty(veri.CihazId))
                return BadRequest(new { message = "Eksik veya hatalı veri." });

            bool cihazVarMi = _context.Cihazlar.Any(c => c.CihazId.ToLower() == veri.CihazId.ToLower());
            if (!cihazVarMi)
                return BadRequest(new { message = "Cihaz ID sistemde kayıtlı değil." });

            if (veri.Deger > 500)
                return BadRequest(new { message = "Değer 500'den büyük olamaz." });

            if (veri.Zaman == default)
                veri.Zaman = DateTime.Now;

            veri.Zaman = DateTime.SpecifyKind(veri.Zaman, DateTimeKind.Unspecified);

               int sonId = 0;
                if (_context.Veriler.Any())
                   {
                    sonId = _context.Veriler.Max(v => v.Id);
                    } 
                     veri.Id = sonId + 1;

            _context.Veriler.Add(veri);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Veri başarıyla eklendi." });
        }
    }
}

