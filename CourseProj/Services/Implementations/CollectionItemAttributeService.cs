using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class CollectionItemAttributeService(ICollectionItemAttributeRepository collectionItemAttributeRepository) : ICollectionItemAttributeService
{
    public async Task<IEnumerable<CollectionItemAttribute>> CreateRows(IEnumerable<CollectionItemAttribute> attributes)
    {
        var attributesAdded = await collectionItemAttributeRepository.CreateRows(attributes);

        return attributesAdded;
    }

    public Task<IEnumerable<CollectionItemAttribute>> UpdateRows(IEnumerable<CollectionItemAttribute> attributes)
    {
        var attributesUpdated = collectionItemAttributeRepository.UpdateRows(attributes);

        return attributesUpdated;
    }

    public IEnumerable<CollectionItemAttribute> GetCollectionAttributes(int id)
    {
        var attributes = collectionItemAttributeRepository.GetCollectionAttributes(id);

        return attributes;
    }
}