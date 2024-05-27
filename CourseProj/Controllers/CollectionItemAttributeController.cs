using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Controllers;

public class CollectionItemAttributeController(ICollectionItemAttributeService collectionItemAttributeService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateRows([FromBody] List<CollectionItemAttribute> attributes)
    {
        await collectionItemAttributeService.CreateRows(attributes);

        return Ok(); 
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateRows([FromBody] List<CollectionItemAttribute> attributes)
    {
        await collectionItemAttributeService.UpdateRows(attributes);

        return Ok(); 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCollectionAttributes(int id)
    {
        // Найти коллекцию по id
        var collection = collectionItemAttributeService.GetCollectionAttributes(id);

        return Ok(collection);
    }
}