using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasincIzlemeProjesi.Models
{
    [Table("veriler")]
    public class SensorData
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

