using System;
using Microservice.Article.Service.Controllers;
using Microservice.Article.Service.DbContexts;
using Microservice.Article.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.Article.Service.Repositories
{
    public class MongoRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public IMongoCollection<ArticleModel> Articles { get; set; }
        public IMongoCollection<CommentModel> Comments { get; set; }

        public MongoRepository(IOptions<ArticleDbContext> articleSettings, IOptions<CommentDbContext> commentSettings)
        {
            try
            {
                var client = new MongoClient(articleSettings.Value.ConnectionString);
                _mongoDatabase = client.GetDatabase(articleSettings.Value.DatabaseName);
                Articles = _mongoDatabase.GetCollection<ArticleModel>(articleSettings.Value.CollectionName);
                Comments = _mongoDatabase.GetCollection<CommentModel>(commentSettings.Value.CollectionName);
            }
            catch (Exception e)
            {
                throw new Exception("Can not access to MongoDb Server.", e);
            }
        }
    }
}