using BasincIzlemeProjesi.Data;
using BasincIzlemeProjesi.Models;
using Microsoft.EntityFrameworkCore;

public class SeedUser
{
    public static void Seed(GirisSistemiContext context)
    {
        if (!context.Kullanicilar.Any())
        {
            context.Kullanicilar.Add(new Kullanicilar
            {
                KullaniciAdi = "kullanici",
                Sifre = "12312"
            });
            context.SaveChanges();
        }
    }
}

