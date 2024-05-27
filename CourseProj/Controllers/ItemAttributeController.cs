using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;

public class ItemAttributeController(IItemAttributeService itemAttributeService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateItemAttribute([FromBody]List<ItemAttribute> itemAttributes)
    {
        await itemAttributeService.CreateItemAttribute(itemAttributes);

        return Ok(); 
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateItemAttributeValue(string value, int id)
    {
        await itemAttributeService.UpdateItemAttributeValue(value, id);
        return Ok();
    }
}