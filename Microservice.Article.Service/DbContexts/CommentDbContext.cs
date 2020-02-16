namespace Microservice.Article.Service.DbContexts
{
    public class CommentDbContext : IDbContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}