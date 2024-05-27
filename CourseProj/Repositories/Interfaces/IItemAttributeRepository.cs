using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface IItemAttributeRepository
{
    Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<ItemAttribute> itemAttributes);

    Task<ItemAttribute> UpdateItemAttributeValue(string value, int id);
}