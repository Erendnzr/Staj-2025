using Microsoft.AspNetCore.Mvc;
using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BasincIzlemeProjesi.Controllers
{
    public class AuthController : Controller
    {
        private readonly GirisSistemiContext _context;

        public AuthController(GirisSistemiContext context)
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

    var user = _context.Kullanicilar
        .FromSqlRaw("SELECT * FROM kullanicilar WHERE kullanici_adi = {0} AND sifre = MD5({1})", model.KullaniciAdi, model.Sifre)
        .AsNoTracking()
        .FirstOrDefault();

    if (user == null)
    {
        ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
        return View(model);
    }

    
    return RedirectToAction("Index", "Monitor");
}

    }
}

