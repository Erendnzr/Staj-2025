using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasincIzlemeProjesi.Models
{
    [Table("veriler")]
    public class Veri
    {
        public int Id { get; set; }

        [Column("cihaz_id")]
        public string CihazId { get; set; }

        [Column("veri")]
        public string Deger { get; set; }  

        [Column("zaman")]
        public DateTime Zaman { get; set; }
    }
}

