using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IItemService
{
    Task<Item> CreateItem(Item item);
    
    Task<Item> CreateItem(string Name, int collectionId);

    Task<Item> GetItemById(int id);

    Task<IQueryable<Item>> GetItemsByQuery(string query);

    Task<Item> UpdateItemName(string value, int id);

    Task DeleteItems(List<int> itemIds);

    Task<IQueryable<Item>> GetLatestItems(int count);
}