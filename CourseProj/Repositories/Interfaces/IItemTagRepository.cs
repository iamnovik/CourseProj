using CourseProj.Data;
using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface IItemTagRepository
{
    Task<ItemTag> CreateItemTag(int itemId, int tagId);

    Task DeleteItemTag(int itemId, int tagId);

    Task<IQueryable<Item>> GetItemsByTagId(int tagId);
}