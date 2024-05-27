using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class CollectionRepository(AppDbContext appDbContext) : ICollectionRepository
{
    public async Task<Collection> GetCollectionById(int id)
    {
        var collection = await appDbContext.Collections.Include(c => c.Items).ThenInclude(i => i.ItemsAttributes).ThenInclude(i => i.ColllectionItemAttribute).ThenInclude(i => i.Attribute)
            .Include(c => c.Items).ThenInclude(i => i.ItemsTags).ThenInclude(it => it.Tag)
            .Include(c => c.ItemsAttributes).ThenInclude(i => i.Attribute)
            .Include(c => c.AppUser)
            .Include(c => c.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        return collection;
    }

    public async Task<Collection> CreateCollection(Collection collection)
    {
        
        appDbContext.Collections.Add(collection);
        
        await appDbContext.SaveChangesAsync();

        return collection;
    }

    public async Task DeleteCollections(List<int> collIds)
    {
        foreach (var id in collIds)
        {
            var coll = appDbContext.Collections.Find(id);
            appDbContext.Collections.Remove(coll!);
        }
        
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Collection> GetCollectionDataById(int id)
    {
        var collection = await appDbContext.Collections.FindAsync(id);

        return collection;
    }

    public async Task<Collection> UpdateCollection(Collection collection)
    {
        
        appDbContext.Collections.Update(collection);
        
        await appDbContext.SaveChangesAsync();

        return collection;
    }

    public async Task<Collection> UpdateCollectionName(string value, int id)
    {
        if (!String.IsNullOrEmpty(value))
        {
            var collection = await appDbContext.Collections.FindAsync(id);
            if (collection != null)
            {
                collection.Name = value;
                appDbContext.Collections.Update(collection);
                await appDbContext.SaveChangesAsync();
                return collection;
            }
        }

        return null;
    }

    public async Task<Collection> UpdateCollectionDescription(string value, int id)
    {
        if (!String.IsNullOrEmpty(value))
        {
            var collection = await appDbContext.Collections.FindAsync(id);
            if (collection != null)
            {
                collection.Description = value;
                appDbContext.Collections.Update(collection);
                await appDbContext.SaveChangesAsync();
                return collection;
            }
        }

        return null;
    }

    public async Task<IQueryable<Collection>> GetLargestCollections(int count)
    {
        var collections = appDbContext.Collections.OrderByDescending(c => c.Items.Count).Take(count).Include(c => c.Items);

        return collections;
    }

    public async Task<IQueryable<Collection>> GetCollectionsByUserId(string id)
    {
        var collections = appDbContext.Collections.Where(c => c.AppUserId == id);
        return collections;
    }
}