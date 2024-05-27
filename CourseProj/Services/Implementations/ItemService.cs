using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class ItemService(IItemRepository itemRepository) : IItemService
{
    public async Task<Item> CreateItem(Item item)
    {
        item.CreatedAt = DateTime.UtcNow;
        var itemAdded = await itemRepository.CreateItem(item);
        //await searchService.IndexItemAsync(itemAdded);
        return itemAdded;

    }

    public async Task<Item> CreateItem(string Name, int collectionId)
    {
        var item = new Item { CreatedAt = DateTime.UtcNow, CollectionId = collectionId, Name = Name };
        item = await itemRepository.CreateItem(item);
        return item;
    }

    public async Task<Item> GetItemById(int id)
    {
        var item = await itemRepository.GetItemById(id);
        return item;
    }

    public async Task<IQueryable<Item>> GetItemsByQuery(string query)
    {
        var items = await itemRepository.GetItemsByQuery(query);

        return items;
    }

    public async Task<Item> UpdateItemName(string value, int id)
    {
        var itemUpdated =await itemRepository.UpdateItemName(value, id);
        
        return itemUpdated;
    }

    public async Task DeleteItems(List<int> itemIds)
    {
        await itemRepository.DeleteItems(itemIds);
    }

    public async Task<IQueryable<Item>> GetLatestItems(int count)
    {
        var items = await itemRepository.GetLatestItems(count);

        return items;
    }
}