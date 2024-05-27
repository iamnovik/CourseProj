using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class ItemRepository(AppDbContext appDbContext) : IItemRepository
{
    public async Task<Item> CreateItem(Item item)
    {

        appDbContext.Items.Add(item);
        
        await appDbContext.SaveChangesAsync();

        return item;
    }

    public async Task<Item> GetItemById(int id)
    {
        var item = await appDbContext.Items.Include(i => i.Collection).ThenInclude(c => c.AppUser)
            .Include(i => i.ItemsAttributes).ThenInclude(ia => ia.ColllectionItemAttribute)
            .ThenInclude(cia => cia.Attribute)
            .Include(i => i.ItemsTags).ThenInclude(t => t.Tag)
            .Include(i => i.Comments).ThenInclude(c => c.AppUser)
            .Include(i => i.Likes)
            .FirstOrDefaultAsync(i => i.Id == id);
        return item;
    }

    public async Task<Item> GetItemDataById(int id)
    {
        var item = appDbContext.Items.Find(id);
        return item;
    }

    public async Task<Item> UpdateItemName(string value, int id)
    {
        if (!String.IsNullOrEmpty(value))
        {
            var item = await appDbContext.Items.FindAsync(id);
            if (item != null)
            {
                item.Name = value;
                appDbContext.Items.Update(item);
                await appDbContext.SaveChangesAsync();
                return item;
            }
        }

        return null;
    }
    
    public async Task<IQueryable<Item>> GetItemsByQuery(string query)
    {
        var items = appDbContext.Items
            .Include(i => i.Comments)
            .Include(i => i.Collection).ThenInclude(c => c.AppUser)
            .Where(p => (p.SearchVector.Matches(query) ||
                         (p.Comments != null && p.Comments.Any(c => c.SearchVector.Matches(query))) ||(p.Collection.SearchVector.Matches(query))));

        

        return items;
    }

    public async Task DeleteItems(List<int> itemIds)
    {
        foreach (var id in itemIds)
        {
            var item = appDbContext.Items.Find(id);
            appDbContext.Items.Remove(item!);
        }
        
        await appDbContext.SaveChangesAsync();
    }

    public async Task<IQueryable<Item>> GetLatestItems(int count)
    {
        var items = appDbContext.Items.OrderByDescending(i => i.CreatedAt).Take(count).Include(i => i.Collection)
            .ThenInclude(c => c.AppUser);
        return items;
    }
}