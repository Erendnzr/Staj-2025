using Microsoft.AspNetCore.Mvc;
using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Models;
using System;
using System.Linq;

namespace BasincIzlemeProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly GirisSistemiContext _context;

        public HomeController(GirisSistemiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            // Son 10 veri
            var son10Veri = _context.Veriler
                .OrderByDescending(v => v.Zaman)
                .Take(10)
                .ToList();

            // Son 10 dakika verileri
            var son10DakikaVerileri = _context.Veriler
                .Where(v => v.Zaman >= now.AddMinutes(-10))
                .OrderBy(v => v.Zaman)
                .ToList();

            // Son 1 saat verileri
            var son1SaatVerileri = _context.Veriler
                .Where(v => v.Zaman >= now.AddHours(-1))
                .OrderBy(v => v.Zaman)
                .ToList();

            var model = new SensorViewModel
            {
                Son10Veri = son10Veri,
                Son10DakikaVerileri = son10DakikaVerileri,
                Son1SaatVerileri = son1SaatVerileri
            };

            return View(model);
        }
    }
}

