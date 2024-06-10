using CourseProj.Models;
using CourseProj.Models.Enums;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CourseProj.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var apiTokenService = serviceProvider.GetRequiredService<IApiTokenService>();
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
            UserName = "novikvlad10@outlook.com",
            Email = "novikvlad10@outlook.com",
                
        };
        var result = await userManager.CreateAsync(user, "admin");

        if (result.Succeeded)
        {
            await apiTokenService.AddTokenAsync(user.Id);
            await userManager.AddToRoleAsync(user, Role.Admin.ToString());
        }
    }
}