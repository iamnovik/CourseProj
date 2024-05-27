using CourseProj.Models;
using CourseProj.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CourseProj.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        IdentityResult roleResult;
        string[] roleNames = new[] { Role.Admin.ToString(), Role.User.ToString() };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Создаем роли и сохраняем их в базе данных
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        AppUser user = new()
        {
            Name = "admin",
            UserName = "admin@admin",
            Email = "admin@admin",
                
        };
        var result = await userManager.CreateAsync(user, "admin");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Role.Admin.ToString());
        }
    }
}