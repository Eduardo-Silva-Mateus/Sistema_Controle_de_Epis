using Controle_de_Epis.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Controle_de_Epis.Infrastructure.Identity
{
    public static class IdentitySeeder
    {

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed roles
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Operador))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Operador));
            }

            // Seed default admin user
            var adminUser = await userManager.FindByEmailAsync("admin@controledeepi.com");

            if (adminUser == null)
            {

                var admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@controledeepi.com",
                    Nome = "Administrador",
                    Ativo = true
                };
                IdentityResult result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
                else
                {
                   result.Errors.ToList().ForEach(e => Console.WriteLine($"Error creating admin user: {e.Description}"));
                }

                
            }

        }
    }
}
