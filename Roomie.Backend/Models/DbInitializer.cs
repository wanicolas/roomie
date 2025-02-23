using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Roomie.Backend.Models;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Vérifier et créer les rôles Admin & User
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Vérifier s'il existe déjà un admin
            if (await userManager.Users.AnyAsync(u => u.Email == "admin@roomie.com")) return;

            // Création d'un compte admin par défaut
            var adminUser = new ApplicationUser
            {
                UserName = "admin@roomie.com",
                Email = "admin@roomie.com",
                EmailConfirmed = true,
                FullName = "Admin Roomie"
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!"); // 🔐 Change le mot de passe si besoin
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
