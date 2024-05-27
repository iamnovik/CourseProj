using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IItemAttributeService
{
    Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<ItemAttribute> itemAttributes);
    
    Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<String> values, int itemId, List<int> collectionItemAttributesIds);

    Task<ItemAttribute> UpdateItemAttributeValue(string value, int id);
}