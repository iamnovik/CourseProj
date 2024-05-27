using System.Collections.ObjectModel;
using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ICollectionRepository
{
    Task<Collection> GetCollectionById(int id);
    Task<Collection> CreateCollection(Collection collection);

    Task DeleteCollections(List<int> collIds);

    Task<Collection> GetCollectionDataById(int id);

    Task<Collection> UpdateCollection(Collection collection);

    Task<Collection> UpdateCollectionName(string value, int id);

    Task<Collection> UpdateCollectionDescription(string value, int id);

    Task<IQueryable<Collection>> GetLargestCollections(int count);

    Task<IQueryable<Collection>> GetCollectionsByUserId(string id);
}