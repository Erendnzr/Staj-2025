using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasincIzlemeProjesi.Models
{
    [Table("cihazlar")]
    public class Cihazlar
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("cihaz_id")]
        public string CihazId { get; set; }

        [Column("cihaz_adi")]
        public string CihazAdi { get; set; }
    }
}
