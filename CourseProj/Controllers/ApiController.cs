using CourseProj.Data;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiController(IApiTokenService apiTokenService, ICollectionService collectionService) : Controller
{
    
    [HttpGet("collections")]
    public async Task<IActionResult> GetCollections([FromQuery] string token)
    {
        var apiToken = await apiTokenService.GetApiTokenByToken(token) ;
        if (apiToken == null)
        {
            return Unauthorized("Invalid API token");
        }

        var collections = await collectionService.GetCollectionsByUserId(apiToken.UserId);

        return Ok(collections);
    }
}