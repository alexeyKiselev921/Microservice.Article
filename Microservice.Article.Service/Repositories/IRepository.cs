using MongoDB.Driver;

namespace Microservice.Article.Service.Repositories
{
    public interface IRepository
    {
        IMongoDatabase Database { get; }
    }
}