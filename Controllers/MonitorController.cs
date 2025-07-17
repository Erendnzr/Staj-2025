using Microsoft.AspNetCore.Mvc;
using BasincIzlemeProjesi.Data;
using System;
using System.Linq;

namespace BasincIzlemeProjesi.Controllers
{
    public class MonitorController : Controller
    {
        private readonly GirisSistemiContext _context;

        public MonitorController(GirisSistemiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var last10 = _context.Veriler
                .OrderByDescending(v => v.Zaman)
                .Take(10)
                .ToList();

            var last10MinCount = _context.Veriler
                .Count(v => v.Zaman >= now.AddMinutes(-10));

            var last1HourCount = _context.Veriler
                .Count(v => v.Zaman >= now.AddHours(-1));

            ViewBag.Last10MinCount = last10MinCount;
            ViewBag.Last1HourCount = last1HourCount;

            return View(last10);
        }
    }
}

