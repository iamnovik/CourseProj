using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class ItemTagService(IItemTagRepository itemTagRepository) : IItemTagService
{
    public async Task<ItemTag> CreateItemTag(int itemId, int tagId)
    {
        var itemTag = await itemTagRepository.CreateItemTag(itemId, tagId);
        
        return itemTag;
    }

    public async Task DeleteItemTag(int itemId, int tagId)
    {
        await itemTagRepository.DeleteItemTag(itemId, tagId);
        
        
    }

    public async Task<IQueryable<Item>> GetItemsByTagId(int tagId)
    {
        var items = await itemTagRepository.GetItemsByTagId(tagId);

        return items;
    }
}