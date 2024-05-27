using CourseProj.Filters;
using CourseProj.Models;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class SearchListController(IItemService itemService, IItemTagService itemTagService) : Controller
{
    public async Task<IActionResult> Index(string query)
    {
        var items = await itemService.GetItemsByQuery(query);

        return View(items.ToList());
    }
    public async Task<IActionResult> SearchTag(int tagId)
    {
        var items = await itemTagService.GetItemsByTagId(tagId);

        return View("Index",items.ToList());
    }
    
}