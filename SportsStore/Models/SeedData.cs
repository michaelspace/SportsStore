using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Kajak",
                    Description = "Łódka przeznaczona dla jednej osoby",
                    Category = "Sporty wodne",
                    Price = 275M
                },
                new Product
                {
                    Name = "Kamizelka ratunkowa",
                    Description = "Chroni przez utonięciem",
                    Category = "Sporty wodne",
                    Price = 48.75M
                },
                new Product
                {
                    Name = "Piłka Adidas",
                    Description = "Piłka do gry zatwierdzona przez FIFA",
                    Category = "Piłka nożna",
                    Price = 19.50M
                },
                new Product
                {
                    Name = "Buty do gry",
                    Description = "Dostępne w kilku wariantach rozmiarowych",
                    Category = "Piłka nożna",
                    Price = 149.99M
                },
                new Product
                {
                    Name = "Rękawice bramkarskie",
                    Description = "Solidna jakość - teraz obrona to czysta przyjemność",
                    Category = "Piłka nożna",
                    Price = 75.25M
                },
                new Product
                {
                    Name = "Składany stadion",
                    Description = "Murawa na 35 000 osób",
                    Category = "Piłka nożna",
                    Price = 79500M
                },
                new Product
                {
                    Name = "Solidna bramka",
                    Description = "Bramka piłkarska polskiej produkcji dla profesjonalistów",
                    Category = "Piłka nożna",
                    Price = 2500M
                },
                new Product
                {
                    Name = "Szachownica wielka i pionki do gry",
                    Description = "Osiągają nawet rozmiary człowieka, przyjemność gwarantowana",
                    Category = "Szachy",
                    Price = 499.99M
                },
                new Product
                {
                    Name = "Szachownica mała i pionki do gry",
                    Description = "Gram którą można wszędzie zabrać",
                    Category = "Szachy",
                    Price = 99.99M
                },
                new Product
                {
                    Name = "Niestabilne krzesło",
                    Description = "Zmniejsza szanse przeciwnika",
                    Category = "Szachy",
                    Price = 29.75M
                },
                new Product
                {
                    Name = "Czapka dla gracza",
                    Description = "Dodaje uroku oraz zwiększa szansę na zwycięstwo",
                    Category = "Szachy",
                    Price = 15M
                }
                );
                context.SaveChanges();
            }
        }
    }
}
