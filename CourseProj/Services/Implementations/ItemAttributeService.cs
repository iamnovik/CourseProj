using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class ItemAttributeService(IItemAttributeRepository itemAttributeRepository) : IItemAttributeService
{
    public async Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<ItemAttribute> itemAttributes)
    {
        var attributes = await itemAttributeRepository.CreateItemAttribute(itemAttributes);

        return attributes;
    }

    public async Task<IEnumerable<ItemAttribute>> CreateItemAttribute(List<string> values, int itemId, List<int> collectionItemAttributesIds)
    {
        var attributes = new List<ItemAttribute>();
        for (int i = 0; i < values.Count; i++)
        {
            attributes.Add(new ItemAttribute{ItemId = itemId, Value = values[i], CollectionItemAttributeId = collectionItemAttributesIds[i]});
        }

        return await itemAttributeRepository.CreateItemAttribute(attributes);
        
    }

    public async Task<ItemAttribute> UpdateItemAttributeValue(string value, int id)
    {
        var attributes = await itemAttributeRepository.UpdateItemAttributeValue(value, id);

        return attributes;
    }
}