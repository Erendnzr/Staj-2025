using BasincIzlemeProjesi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasincIzlemeProjesi.Data
{
    public class GirisSistemiContext : DbContext
    {
        public GirisSistemiContext(DbContextOptions<GirisSistemiContext> options)
            : base(options)
        {
        }

        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Veri> Veriler { get; set; }
        public DbSet<Cihazlar> Cihazlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanicilar>().ToTable("kullanicilar");

            modelBuilder.Entity<Veri>(entity =>
            {
                entity.ToTable("veriler");
                entity.Property(e => e.CihazId).HasColumnName("cihaz_id");
                entity.Property(e => e.Deger).HasColumnName("Veri");
                entity.Property(e => e.Zaman).HasColumnName("zaman");
            });

            modelBuilder.Entity<Cihazlar>(entity =>
            {
                entity.ToTable("cihazlar");
                entity.Property(e => e.CihazId).HasColumnName("cihaz_id");
                entity.Property(e => e.CihazAdi).HasColumnName("cihaz_adi");
            });
        }
    }
}

