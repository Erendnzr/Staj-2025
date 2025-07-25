using System.Collections.Generic;

namespace BasincIzlemeProjesi.Models
{
    public class SensorViewModel
    {
        public List<Veri> Son10Veri { get; set; }
        public List<Veri> Son10DakikaVerileri { get; set; }
        public List<Veri> Son1SaatVerileri { get; set; }
    }
}
