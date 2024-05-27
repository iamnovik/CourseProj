using Amazon.S3;
using Amazon.S3.Transfer;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;

    public CollectionService(ICollectionRepository collectionRepository, IConfiguration configuration )
    {
        _collectionRepository = collectionRepository;
        _configuration = configuration;
        
        var s3Config = new AmazonS3Config
        {
            ServiceURL = _configuration["YandexCloud:ServiceUrl"],
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(
            _configuration["YandexCloud:AccessKey"],
            _configuration["YandexCloud:SecretKey"],
            s3Config
        );
    }
    public async Task<Collection> GetCollectionById(int id)
    {
        var collection = await _collectionRepository.GetCollectionById(id);
        return collection;
    }

    public async Task<Collection> CreateCollection(Collection collection, string id, IFormFile image)
    {
        collection.AppUserId = id;
        
        if (image != null)
        {
            var bucketName = _configuration["YandexCloud:BucketName"];
            
            var keyName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            
            var imageUrl = await SaveCollectionImage(image, bucketName, keyName);
            collection.ImageUrl = imageUrl;
        }

        var collectionAdded = await _collectionRepository.CreateCollection(collection);

        return collectionAdded;
    }

    public async Task DeleteCollections(List<int> collIds)
    {
        await _collectionRepository.DeleteCollections(collIds);
    }

    public async Task<Collection> GetCollectionDataById(int id)
    {
        var collection = await _collectionRepository.GetCollectionDataById(id);
        return collection;
    }

    public async Task<string> SaveCollectionImage(IFormFile image, string? bucketName, string keyName)
    {
        using (var stream = new MemoryStream())
        {
            await image.CopyToAsync(stream);
            var request = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = keyName,
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            var transferUtility = new TransferUtility(_s3Client);
            await transferUtility.UploadAsync(request);
        }
        
        return $"https://{bucketName}.storage.yandexcloud.net/{keyName}";
    }
    

    public async Task<Collection> UpdateCollectionName(string value, int id)
    {
        var collection = await _collectionRepository.GetCollectionDataById(id);
        collection.Name = value;
        collection = await _collectionRepository.UpdateCollection(collection);

        return collection;
    }

    public async Task<Collection> UpdateCollectionImage(IFormFile image, int id)
    {
        var collection = await _collectionRepository.GetCollectionDataById(id);
        var bucketName = _configuration["YandexCloud:BucketName"];
            
        var keyName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            
        collection.ImageUrl = await SaveCollectionImage(image, bucketName, keyName);
        collection = await _collectionRepository.UpdateCollection(collection);
        return collection;

    }

    public async Task<Collection> UpdateCollectionDescription(string value, int id)
    {
        var collection = await _collectionRepository.GetCollectionDataById(id);
        collection.Description = value;
        collection = await _collectionRepository.UpdateCollection(collection);
        return collection;
    }

    public async Task<IQueryable<Collection>> GetLargeestCollections(int count)
    {
        var collections = await _collectionRepository.GetLargestCollections(count);

        return collections;
    }

    public async Task<IQueryable<Collection>> GetCollectionsByUserId(string id)
    {
        var collections = await _collectionRepository.GetCollectionsByUserId(id);

        return collections;
    }
}