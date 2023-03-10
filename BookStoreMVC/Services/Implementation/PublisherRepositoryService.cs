using BookStoreMVC.DataAccess;
using BookStoreMVC.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreMVC.Services.Implementation;

public class PublisherRepositoryService : IPublisherRepository
{
    private readonly IMongoCollection<Publisher> _publisherCollection;
    public PublisherRepositoryService(IOptions<BookStoreDataAccess> dataAccess)
    {
        var mongoClient = new MongoClient(dataAccess.Value.ConnectionString);
        var database = mongoClient.GetDatabase(dataAccess.Value.DatabaseName);
        _publisherCollection = database.GetCollection<Publisher>(dataAccess.Value.PublisherCollectionName);
    }
    public IEnumerable<Publisher> GetAll()
    {
        return _publisherCollection.Find(_ => true).ToEnumerable();
    }

    public Publisher GetById(string publisherId)
    {
        return _publisherCollection.Find(publisher => publisher.Id == publisherId).FirstOrDefault();
    }

    public Task<Publisher> GetWithFilterAsync(FilterDefinition<Publisher> filterDefinition)
    {
        return _publisherCollection.Find(filterDefinition).FirstOrDefaultAsync();
    }

    public Publisher Add()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Publisher document)
    {
        try
        {
            await _publisherCollection.InsertOneAsync(document);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Publisher Update(string publisherId)
    {
        throw new NotImplementedException();
    }

    public Publisher Delete(string publisherId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string id) =>
      await _publisherCollection.DeleteOneAsync(x => x.Id == id);
}