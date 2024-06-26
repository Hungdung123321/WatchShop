using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WatchShop.Areas.Identity.Data;

namespace WatchShop.Models
{
    public static class SeedUser
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<WatchShopUser>>();

            string[] roleNames = { "admin", "client","seller" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var AdminEmail = "Admin1@gmail.com";
            var SellerEmail = "Seller1@gmail.com";
            var ClientEmail = "Client1@gmail.com";
            var PasswordForAll = "Dung123321@";

            if (userManager.FindByEmailAsync(AdminEmail).Result == null)
            {
                WatchShopUser user = new()
                {
                    Email = AdminEmail,
                    UserName = AdminEmail,
                    Name = "Admin1",
                    EmailConfirmed = true,
                };

                IdentityResult result = userManager.CreateAsync(user, PasswordForAll).Result;

                if (result.Succeeded)
                {
                     userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync(SellerEmail).Result == null)
            {
                WatchShopUser user = new()
                {
                    Email = SellerEmail,
                    UserName = SellerEmail,
                    Name = "Seller1",
                    EmailConfirmed = true,
                };

                IdentityResult result = userManager.CreateAsync(user, PasswordForAll).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "seller").Wait();
                }
            }

            if (userManager.FindByEmailAsync(ClientEmail).Result == null)
            {
                WatchShopUser user = new()
                {
                    Email = ClientEmail,
                    UserName = ClientEmail,
                    Name = "Client1",
                    EmailConfirmed = true,
                };

                IdentityResult result = userManager.CreateAsync(user, PasswordForAll).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "client").Wait();
                }
            }

        }
    }
}