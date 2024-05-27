using CourseProj.Data;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Controllers;

public class AttributeController(IAttributeService attributeService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAttributes([FromBody]List<Attribute> attributes)
    {
        var attributesAdded = await attributeService.CreateAttributes(attributes);
        var attributeIds = attributesAdded.Select(a => a.Id).ToList();

        return Ok(attributeIds); 
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateAttributes([FromBody]List<Attribute> attributes)
    {
        var attributesUpdated = attributeService.UpdateAttributes(attributes);
        var attributeIds = attributes.Select(a => a.Id).ToList();

        return Ok(attributeIds); 
    }
    
  
}