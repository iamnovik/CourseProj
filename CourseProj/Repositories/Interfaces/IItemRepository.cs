using CourseProj.Models;


namespace CourseProj.Repositories.Interfaces;

public interface IItemRepository
{
    Task<Item> CreateItem(Item item);

    Task<Item> GetItemById(int id);

    Task<Item> GetItemDataById(int id);

    Task<IQueryable<Item>> GetItemsByQuery(string query);

    Task<Item> UpdateItemName(string value, int id);

    Task DeleteItems(List<int> itemIds);

    Task<IQueryable<Item>> GetLatestItems(int count);
}