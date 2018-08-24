using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "_$Admin$1$2$6$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            // UserManager - manager do zarzadzania uzytkownikami
            // IdentityUser - domyslna implementacja uzytkownika pozwalajaca uzyc string jako kluczy

            // Manager przeszukuje store uzytkownikow po string kluczu
            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                // Dodaje nowego uzytkownika w przypadku nieznalezienia usera o string kluczu w store
                user = new IdentityUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
