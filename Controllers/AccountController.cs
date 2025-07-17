using Microsoft.AspNetCore.Mvc;
using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Dtos;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BasincIzlemeProjesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly GirisSistemiContext _context;

        public AccountController(GirisSistemiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            string hashedPassword = GetMd5Hash(model.Sifre);

            var user = _context.Kullanicilar
                .FirstOrDefault(u => u.KullaniciAdi == model.KullaniciAdi && u.Sifre == hashedPassword);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
                return View(model);
            }

            return RedirectToAction("Index", "Monitor");
        }

        // MD5 Şifreleme Fonksiyonu
        private string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}

