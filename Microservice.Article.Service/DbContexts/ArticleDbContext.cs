namespace Microservice.Article.Service.DbContexts
{
    public class ArticleDbContext : IDbContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}