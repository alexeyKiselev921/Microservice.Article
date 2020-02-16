namespace Microservice.Article.Service.DbContexts
{
    public interface IDbContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}