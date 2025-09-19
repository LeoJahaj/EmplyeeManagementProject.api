using System.Threading.Tasks;
using EmployeeManagement.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.api.Infrastructure
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Employee))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employee));
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var email = configuration["Admin:Email"]
                        ?? Environment.GetEnvironmentVariable("ADMIN__EMAIL")
                        ?? "admin@local";
            var password = configuration["Admin:Password"]
                           ?? Environment.GetEnvironmentVariable("ADMIN__PASSWORD")
                           ?? "Admin@12345";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(newUser, password);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, Roles.Administrator);
                }
            }
        }
    }
}


