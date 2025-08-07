using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasincIzlemeProjesi.Controllers
{
   
    public class MonitorController : Controller
    {
        private readonly GirisSistemiContext _context;
        private const int SayfaBoyutu = 100;

        public MonitorController(GirisSistemiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tip = "son10", int sayfa = 1, DateTime? baslangic = null, DateTime? bitis = null)
        {
            IQueryable<Veri> sorgu = _context.Veriler.AsQueryable();
            var now = DateTime.UtcNow;

            bool sayfalamaVar = false;
            List<Veri> veriler = new List<Veri>();

            if (tip.ToLower() == "tumveriler")
            {
                // Tarih aralığı ile filtrele
                if (baslangic.HasValue)
                    sorgu = sorgu.Where(v => v.Zaman >= baslangic.Value);

                if (bitis.HasValue)
                    sorgu = sorgu.Where(v => v.Zaman <= bitis.Value);

                sorgu = sorgu.OrderBy(v => v.Zaman);

                sayfalamaVar = true;

                int toplamKayit = await Task.Run(() => sorgu.Count());

                int toplamSayfa = (int)Math.Ceiling((double)toplamKayit / SayfaBoyutu);

                if (sayfa < 1) sayfa = 1;
                if (sayfa > toplamSayfa) sayfa = toplamSayfa;

                veriler = await Task.Run(() => sorgu.Skip((sayfa - 1) * SayfaBoyutu).Take(SayfaBoyutu).ToList());

                ViewBag.ToplamSayfa = toplamSayfa;
                ViewBag.Sayfa = sayfa;
            }
            else
            {
                switch (tip.ToLower())
                {
                    case "son10":
                        veriler = await Task.Run(() => sorgu.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman).ToList());
                        break;

                    case "son10dakika":
                        veriler = await Task.Run(() => sorgu.Where(v => v.Zaman >= now.AddMinutes(-10)).OrderBy(v => v.Zaman).ToList());
                        break;

                    case "son1saat":
                        veriler = await Task.Run(() => sorgu.Where(v => v.Zaman >= now.AddHours(-1)).OrderBy(v => v.Zaman).ToList());
                        break;

                    default:
                        veriler = await Task.Run(() => sorgu.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman).ToList());
                        break;
                }
            }

            var grafikZaman = veriler.Select(v => v.Zaman.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")).ToList();
            var grafikDeger = veriler.Select(v => v.Deger).ToList();

            ViewBag.Tip = tip;
            ViewBag.SayfalamaVar = sayfalamaVar;
            ViewBag.GrafikZamanJson = grafikZaman.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(grafikZaman) : "null";
            ViewBag.GrafikDegerJson = grafikDeger.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(grafikDeger) : "null";
            ViewBag.Baslangic = baslangic?.ToString("yyyy-MM-ddTHH:mm") ?? "";
            ViewBag.Bitis = bitis?.ToString("yyyy-MM-ddTHH:mm") ?? "";

            return View(veriler);
        }
    }
}

