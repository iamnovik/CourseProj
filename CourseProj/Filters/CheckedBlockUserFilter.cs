using CourseProj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseProj.Filters;

public class CheckBlockedUserFilter : IAsyncActionFilter
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public CheckBlockedUserFilter(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Получаем текущего пользователя
        var user = await _userManager.GetUserAsync(context.HttpContext.User);

        if (user != null && user.IsBlocked ) 
        {
            // Если пользователь заблокирован, выполняем выход и перенаправляем на страницу входа
            await _signInManager.SignOutAsync();
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl = context.HttpContext.Request.Path });
            return;
        }

        // Продолжаем выполнение запроса
        await next();
    }
}