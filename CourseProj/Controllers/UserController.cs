using System.Security.Claims;
using CourseProj.Filters;
using CourseProj.Models;
using CourseProj.Services.Implementations;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;

[Authorize]
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class UserController(IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        var user = await userService.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var isInRole = await userManager.IsInRoleAsync(user, "Admin");
        if (isInRole)
        {
            Console.WriteLine("Admin");
            var users = await userService.GetUsers();
            return View(users);
        }

        return RedirectToAction("Index", "Home");

    }
    
    [HttpPost]
    public async Task<IActionResult> GiveAdminRole(List<string> userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userService.GetUserById(id);
            await userManager.AddToRoleAsync(user, "Admin");

        }

        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateUserName(string value, string id)
    {
        var userUpdated = await userService.UpdateUserName(value, id);
        return Ok(userUpdated);


    }
    
    [HttpPost]
    public async Task<IActionResult> TakeAdminRole(List<string> userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userService.GetUserById(id);
            var isInRole = await userManager.IsInRoleAsync(user, "Admin");
            if (isInRole)
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
            }
        }

        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> BlockUsers(List<string> userIds)
    {

        foreach (var userId in userIds)
        {
            var user = await userService.GetUserById(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                userManager.UpdateAsync(user).Wait();
            }

 
        }

        return Ok();
       
    }
    
    [HttpPost]
    public async Task<IActionResult> UnblockUsers(List<string> userIds)
    {

        foreach (var userId in userIds)
        {
            var user = await userService.GetUserById(userId);
            if (user != null)
            {
                user.IsBlocked = false;
                userManager.UpdateAsync(user).Wait();
            }
        }

        return Ok();
       
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUsers(List<string> userIds)
    {
        bool isDeletedHimself = false;

        foreach (var userId in userIds)
        {
            var user = await userService.GetUserById(userId);
            if (user != null)
            {
                userManager.DeleteAsync(user).Wait();
                if (User.Identity.Name == user.UserName)
                {
                    isDeletedHimself = true;
                }
            }
        }

        if (isDeletedHimself)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        return Ok();
        
        
    }
}