using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class ItemTagRepository(AppDbContext appDbContext) : IItemTagRepository
{
    public async Task<ItemTag> CreateItemTag(int itemId, int tagId)
    {
        var itemTag = appDbContext.ItemTags.Find(itemId, tagId);
        if (itemTag == null)
        {
            itemTag = new ItemTag { ItemId = itemId, TagId = tagId };
            appDbContext.ItemTags.Add(itemTag);
            await appDbContext.SaveChangesAsync();
        }
        
        return itemTag;
    }

    public async Task DeleteItemTag(int itemId, int tagId)
    {
        var itemTag = appDbContext.ItemTags.Find(itemId, tagId);
        if (itemTag != null)
        {
            appDbContext.ItemTags.Remove(itemTag);
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task<IQueryable<Item>> GetItemsByTagId(int tagId)
    {
        var items = appDbContext.ItemTags.Include(it => it.Item).ThenInclude(i => i.Collection).ThenInclude(c => c.AppUser)
            .Where(it => it.TagId == tagId)
            .Select(it => it.Item);

        return items;
    }
}