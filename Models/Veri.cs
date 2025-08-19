using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BasincIzlemeProjesi.Models
{
[Table("veriler")]
    public class Veri
    {
        public int Id { get; set; }

        [JsonPropertyName("cihaz_id")]
        [Column("cihaz_id")]
        public string CihazId { get; set; } 

        public int Deger { get; set; }

        public DateTime Zaman { get; set; }  
    }
}

