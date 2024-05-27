using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Services;
using CourseProj.Services.Implementations;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Controllers;

public class TagController(ITagService tagService) : Controller
{
    
    [HttpGet]
    public async Task<IActionResult> GetTagsStartsWith(string query)
    {
        var tags = await tagService.GetTagsStartsWith(query);
        // Получение тегов из базы данных, которые соответствуют запросу

        // Возвращение тегов в формате JSON
        return Json(tags);
    }

    [HttpPost]
    public async Task<IActionResult> AddTag(string name)
    {

        return Ok(tagService.CreateTag(name));
    }
    
    
    
}