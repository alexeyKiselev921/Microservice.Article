using System;
using Microservice.Article.Service.DbContexts;
using Microservice.Article.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.Article.Service.Repositories
{
    public class ArticleRepository : IRepository
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<ArticleModel> Articles { get; set; }

        public ArticleRepository(IOptions<ArticleDbContext> articleSettings)
        {
            try
            {
                var client = new MongoClient(articleSettings.Value.ConnectionString);
                Database = client.GetDatabase(articleSettings.Value.DatabaseName);
                Articles = Database.GetCollection<ArticleModel>(articleSettings.Value.CollectionName);
            }
            catch (Exception e)
            {
                throw new Exception("Can not access to MongoDb Server.", e);
            }
        }
    }
}