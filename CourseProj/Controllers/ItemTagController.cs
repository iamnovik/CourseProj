using CourseProj.Services.Implementations;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;

public class ItemTagController(ITagService tagService,IItemTagService itemTagService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateItemTag(int itemId, string tag)
    {
        var tagAdded = await tagService.CreateTag(tag);
        
        var itemTagAdded = await itemTagService.CreateItemTag(itemId, tagAdded.Id);
        return Ok(itemTagAdded);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteItemTag(int itemId, int tagId)
    {
        await itemTagService.DeleteItemTag(itemId, tagId);
        return Ok();
    }
}