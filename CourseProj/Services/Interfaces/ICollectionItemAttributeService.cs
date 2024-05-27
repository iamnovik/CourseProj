using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ICollectionItemAttributeService
{
    Task<IEnumerable<CollectionItemAttribute>> CreateRows(IEnumerable<CollectionItemAttribute> attributes);

    Task<IEnumerable<CollectionItemAttribute>> UpdateRows(IEnumerable<CollectionItemAttribute> attributes);

    IEnumerable<CollectionItemAttribute> GetCollectionAttributes(int id);
}