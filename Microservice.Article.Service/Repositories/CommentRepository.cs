using System;
using Microservice.Article.Service.Controllers;
using Microservice.Article.Service.DbContexts;
using Microservice.Article.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservice.Article.Service.Repositories
{
    public class CommentRepository : IRepository
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<ArticleModel> Articles { get; set; }
        public IMongoCollection<CommentModel> Comments { get; set; }

        public CommentRepository(IOptions<CommentDbContext> commentSettings)
        {
            try
            {
                var client = new MongoClient(commentSettings.Value.ConnectionString);
                Database = client.GetDatabase(commentSettings.Value.DatabaseName);
                Comments = Database.GetCollection<CommentModel>(commentSettings.Value.CollectionName);
            }
            catch (Exception e)
            {
                throw new Exception("Can not access to MongoDb Server.", e);
            }
        }
    }
}