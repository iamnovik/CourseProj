using CourseProj.Data;
using CourseProj.Filters;
using CourseProj.Hubs;
using CourseProj.Models;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CourseProj.Controllers;
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class ItemController(IItemService itemService, UserManager<AppUser> userManager, ILikeService likeService, ICommentService commentService, IHubContext<CommentHub> commentHub) : Controller
{
    public async Task<IActionResult> Index(int id)
    {

        var item = await itemService.GetItemById(id);
        ViewBag.Collection = item.Collection!.Name;
        return View(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody]Item item)
    {
        var itemAdded = await itemService.CreateItem(item);

        return Ok(itemAdded.Id); 
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> UpdateItemName(string value, int id)
    {
        var itemUpdated = itemService.UpdateItemName(value, id);
        return Ok(itemUpdated);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteItems(List<int> itemIds)
    {
        await itemService.DeleteItems(itemIds);

        return Ok(); 
    }
    
    [HttpPost]
    public async Task<IActionResult> AddLike(int itemId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var like = await likeService.CreateLike(itemId, user.Id);
        if (like == null)
        {
            return BadRequest("You have already liked this item.");
        }

        return RedirectToAction("Index", new { id = itemId });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteLike(int itemId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        await likeService.DeleteLike(itemId, user.Id);

        return RedirectToAction("Index", new { id = itemId });
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int itemId, string text)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        await commentService.CreateComment(itemId, user.Id, text);
        await commentHub.Clients.Group(itemId.ToString()).SendAsync("ReceiveComment", user.Name, text);
        return RedirectToAction("Index", new { id = itemId });
    }
}