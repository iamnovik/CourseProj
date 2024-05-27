using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class CollectionItemAttributeRepository(AppDbContext appDbContext) : ICollectionItemAttributeRepository
{
    public async Task<IEnumerable<CollectionItemAttribute>> CreateRows(IEnumerable<CollectionItemAttribute> attributes)
    {
        foreach (var item in attributes)
        {
            appDbContext.CollectionItemAtributes.Add(item);
        }
        
        await appDbContext.SaveChangesAsync();

        return attributes;
    }

    public async Task<IEnumerable<CollectionItemAttribute>> UpdateRows(IEnumerable<CollectionItemAttribute> attributes)
    {
        foreach (var item in attributes)
        {
            appDbContext.CollectionItemAtributes.Update(item);
        }
        
        await appDbContext.SaveChangesAsync();

        return attributes;
    }

    public IEnumerable<CollectionItemAttribute> GetCollectionAttributes(int id)
    {
        var collection = appDbContext.CollectionItemAtributes.
            Include(c => c.Attribute).
            AsSplitQuery().
            Where(i => i.CollectionId == id);

        return collection;
    }
}