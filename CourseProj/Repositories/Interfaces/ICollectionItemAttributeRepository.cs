using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ICollectionItemAttributeRepository
{
    Task<IEnumerable<CollectionItemAttribute>> CreateRows(IEnumerable<CollectionItemAttribute> attributes);

    Task<IEnumerable<CollectionItemAttribute>> UpdateRows(IEnumerable<CollectionItemAttribute> attributes);

    IEnumerable<CollectionItemAttribute> GetCollectionAttributes(int id);
}