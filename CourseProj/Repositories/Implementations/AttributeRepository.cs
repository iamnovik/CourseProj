using CourseProj.Models;

using CourseProj.Data;
using CourseProj.Repositories.Interfaces;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Repositories.Implementations;

public class AttributeRepository(AppDbContext appDbContext) : IAttributeRepository
{


    public async Task<List<Attribute>> CreateAttributes(List<Attribute> attributes)
    {
        foreach (var item in attributes)
        {
            appDbContext.Attributes.Add(item);
        }
        
        await appDbContext.SaveChangesAsync();
        return attributes;
    }

    public async Task<List<Attribute>> UpdateAttributes(List<Attribute> attributes)
    {
        foreach (var item in attributes)
        {
            appDbContext.Attributes.Update(item);
        }
        
        await appDbContext.SaveChangesAsync();
        return attributes;
    }

}