using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IItemTagService
{
    Task<ItemTag> CreateItemTag(int itemId, int tagId);

    Task DeleteItemTag(int itemId, int tagId);

    Task<IQueryable<Item>> GetItemsByTagId(int tagId);
}