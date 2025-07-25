using System;
using System.Collections.Generic;

namespace BasincIzlemeProjesi.Models
{
    public class TumVerilerViewModel
    {
        public List<Veri> Veriler { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
