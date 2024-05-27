using CourseProj.Models;
using CourseProj.Models.Enums;
using CourseProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;

public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVm model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && user.IsBlocked)
            {
                ModelState.AddModelError("", "Your account has been blocked.");
                return View(model);
            }
            //login
            var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, false, false);
            
            if (result.Succeeded)
            {
                await userManager.UpdateAsync(user);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid login attempt");
        }
        return View(model);
    }

    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                
            };
            
            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                if (model.AdminRole)
                {
                    await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
                else
                {
                    await userManager.AddToRoleAsync(user, Role.User.ToString());
                }
                
                
                await signInManager.SignInAsync(user, false);

                return RedirectToLocal(returnUrl);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction("Index","Home");
    }
}