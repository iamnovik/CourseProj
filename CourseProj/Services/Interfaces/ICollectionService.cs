using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ICollectionService
{
    Task<Collection> GetCollectionById(int id);
    Task<Collection> CreateCollection(Collection collection, string id, IFormFile image);

    Task DeleteCollections(List<int> collIds);

    Task<Collection> GetCollectionDataById(int id);
    

    Task<Collection> UpdateCollectionName(string value, int id);
    
    Task<Collection> UpdateCollectionImage(IFormFile image, int id);

    Task<Collection> UpdateCollectionDescription(string value, int id);

    Task<IQueryable<Collection>> GetLargeestCollections(int count);

    Task<IQueryable<Collection>> GetCollectionsByUserId(string id);
}