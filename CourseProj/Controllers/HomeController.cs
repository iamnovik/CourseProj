using System.Diagnostics;
using CourseProj.Data;
using CourseProj.Filters;
using Microsoft.AspNetCore.Mvc;
using CourseProj.Models;
using CourseProj.Services.Interfaces;
using CourseProj.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Controllers;
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class HomeController(IItemService itemService, ICollectionService collectionService, ITagService tagService) : Controller
{


    public async Task<IActionResult> Index()
    {
        var latestItems = await GetLatestItems(5);
        var largestCollections = await GetLargestCollections(5);
        var tags = await tagService.GetTags();

        // Передайте эти данные в представление
        return View(new HomeVM { LatestItems = latestItems, LargestCollections = largestCollections, Tags = tags.ToList()});
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append("SelectedCulture", culture, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
        return LocalRedirect(returnUrl);
    }
    
    [HttpPost]
    public IActionResult SetTheme(string theme, string returnUrl)
    {
        Response.Cookies.Append("SelectedTheme", theme, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return LocalRedirect(returnUrl);
    }
    
    private async Task<List<Item>> GetLatestItems(int count)
    {
        var items = await itemService.GetLatestItems(count);
        // Получите последние добавленные айтемы из базы данных
        return items.ToList();
    }

    private async Task<List<Collection>> GetLargestCollections(int count)
    {
        var collections = await collectionService.GetLargeestCollections(count);
        return collections.ToList();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}