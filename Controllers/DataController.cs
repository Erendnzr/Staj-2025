using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BasincIzlemeProjesi.Controllers
{
    [Route("Data")]
    public class DataController : Controller
    {
        private readonly GirisSistemiContext _context;

        public DataController(GirisSistemiContext context)
        {
            _context = context;
        }

        [HttpGet("GetData")]
        public IActionResult GetData(string type)
        {
            var now = DateTime.Now;

            var query = type?.ToLower() switch
            {
                "last10" => _context.Veriler
                    .Where(v => v.Deger >= 0 && v.Deger <= 500)
                    .OrderByDescending(v => v.Zaman)
                    .Take(10)
                    .OrderBy(v => v.Zaman),

                "last10minutes" => _context.Veriler
                    .Where(v => v.Zaman >= now.AddMinutes(-10) && v.Deger >= 0 && v.Deger <= 500)
                    .OrderBy(v => v.Zaman),

                "last1hour" => _context.Veriler
                    .Where(v => v.Zaman >= now.AddHours(-1) && v.Deger >= 0 && v.Deger <= 500)
                    .OrderBy(v => v.Zaman),

                _ => _context.Veriler
                    .Where(v => v.Deger >= 0 && v.Deger <= 500)
                    .OrderByDescending(v => v.Zaman)
                    .Take(10)
                    .OrderBy(v => v.Zaman),
            };

            var data = query
                .Select(v => new DataPoint 
                { 
                    Timestamp = v.Zaman, 
                    Value = v.Deger 
                })
                .ToList();

            return Json(data);
        }
    }

    public class DataPoint
    {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }
}

