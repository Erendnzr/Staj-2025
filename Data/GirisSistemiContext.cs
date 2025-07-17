using Microsoft.EntityFrameworkCore;
using BasincIzlemeProjesi.Models;

namespace BasincIzlemeProjesi.Data
{
    public class GirisSistemiContext : DbContext
    {
        public GirisSistemiContext(DbContextOptions<GirisSistemiContext> options)
            : base(options) { }

        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Veri> Veriler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanicilar>().ToTable("kullanicilar");
            modelBuilder.Entity<Veri>().ToTable("veriler");
        }
    }
}

