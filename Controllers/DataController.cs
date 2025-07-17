using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasincIzlemeProjesi.Controllers
{
    [Route("Data")]
    public class DataController : Controller
    {
        private static readonly List<Veri> allData = new()
        {
            new Veri { CihazId = "Cihaz1", Deger = "100", Zaman = DateTime.Now.AddMinutes(-50) },
            new Veri { CihazId = "Cihaz1", Deger = "102", Zaman = DateTime.Now.AddMinutes(-40) },
            new Veri { CihazId = "Cihaz1", Deger = "101", Zaman = DateTime.Now.AddMinutes(-30) },
            new Veri { CihazId = "Cihaz1", Deger = "105", Zaman = DateTime.Now.AddMinutes(-20) },
            new Veri { CihazId = "Cihaz1", Deger = "103", Zaman = DateTime.Now.AddMinutes(-10) },
            new Veri { CihazId = "Cihaz1", Deger = "104", Zaman = DateTime.Now.AddMinutes(-5) },
            new Veri { CihazId = "Cihaz1", Deger = "107", Zaman = DateTime.Now }
        };

        [HttpGet("GetData")]
        public IActionResult GetData(string type)
        {
            var now = DateTime.Now;

            var query = type?.ToLower() switch
            {
                "last10" => allData.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman),
                "last10minutes" => allData.Where(v => v.Zaman >= now.AddMinutes(-10)).OrderBy(v => v.Zaman),
                "last1hour" => allData.Where(v => v.Zaman >= now.AddHours(-1)).OrderBy(v => v.Zaman),
                _ => allData.OrderByDescending(v => v.Zaman).Take(10).OrderBy(v => v.Zaman),
            };

            var data = query.Select(v =>
            {
                int val;
                int.TryParse(v.Deger, out val);
                return new DataPoint
                {
                    Timestamp = v.Zaman,
                    Value = val
                };
            }).ToList();

            return Json(data);
        }
    }

    public class Veri
    {
        public string CihazId { get; set; }
        public string Deger { get; set; }
        public DateTime Zaman { get; set; }
    }

    public class DataPoint
    {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }
}

