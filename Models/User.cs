using System.ComponentModel.DataAnnotations.Schema;

namespace BasincIzlemeProjesi.Models
{
    [Table("kullanicilar")]  
    public class Kullanicilar
    {
        public int Id { get; set; }

        [Column("kullanici_adi")]
        public string KullaniciAdi { get; set; }

        [Column("sifre")]
        public string Sifre { get; set; }
    }
}

