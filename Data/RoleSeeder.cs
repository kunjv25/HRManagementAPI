using HRManagementAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HRManagementAPI.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles that should exist in the application
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                // Check whether the role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Find existing admin user
            var user = await userManager.FindByEmailAsync("admin@gmail.com");

            // Assign Admin role if user exists and is not already an Admin
            if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}