using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;

namespace CourseProj.Repositories.Implementations;

public class ItemAttributeRepository(AppDbContext appDbContext) : IItemAttributeRepository
{
    public async Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<ItemAttribute> itemAttributes)
    {
        foreach (var item in itemAttributes)
        {
            appDbContext.ItemAttributes.Add(item);
        }
        
        await appDbContext.SaveChangesAsync();
        
        return itemAttributes;
    }

    public async Task<ItemAttribute> UpdateItemAttributeValue(string value, int id)
    {
        if (!String.IsNullOrEmpty(value))
        {
            var attribute = await appDbContext.ItemAttributes.FindAsync(id);
            if (attribute != null)
            {
                attribute.Value = value;
                appDbContext.ItemAttributes.Update(attribute);
                await appDbContext.SaveChangesAsync();
                return attribute;
            }
        }

        return null;
    }
}