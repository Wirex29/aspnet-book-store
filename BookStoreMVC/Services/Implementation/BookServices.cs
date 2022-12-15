using BookStoreMVC.DataAccess;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStoreMVC.Services.Implementation;

public class BookServices : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;
    public BookServices(IOptions<BookStoreDataAccess> dataAccess)
    {

        var mongoClient = new MongoClient(
            dataAccess.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dataAccess.Value.DatabaseName);

        _bookCollection = mongoDatabase.GetCollection<Book>(
            dataAccess.Value.BookCollectionName);
    }
    public IEnumerable<Book> GetAll(string filter)
    {
        return _bookCollection.Find(filter => true).ToEnumerable();
    }

    public async Task<Book> GetById(string bookId)
    {
        return await _bookCollection.Find(x => x.Id == bookId).FirstOrDefaultAsync();
        
    }

    public IActionResult Add(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Book book)
    {
        await _bookCollection.InsertOneAsync(book);
    }

    public IActionResult Update(Book book)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete(string bookId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string id) =>
       await _bookCollection.DeleteOneAsync(x => x.Id == id);
}