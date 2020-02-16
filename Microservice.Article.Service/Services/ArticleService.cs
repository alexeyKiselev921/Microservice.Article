using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Article.Service.DbContexts;
using Microservice.Article.Service.Models;
using Microservice.Article.Service.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Microservice.Article.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleRepository _repository = null;

        public ArticleService(IOptions<ArticleDbContext> settings)
        {
            _repository = new ArticleRepository(settings);
        }
        public async Task<IEnumerable<ArticleModel>> GetAll()
        {
            return await _repository.Articles.Find(x => true).ToListAsync();
        }

        public async Task<ArticleModel> Get(string articleId)
        {
            var filter = Builders<ArticleModel>.Filter.Eq("id", articleId);
            return await _repository.Articles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(ArticleModel article)
        {
            await _repository.Articles.InsertOneAsync(article);
        }

        public async Task<bool> Update(ArticleModel article)
        {
            var filter = Builders<ArticleModel>.Filter.Eq("id", article.Id);
            var updatingUser = _repository.Articles.Find(filter).FirstOrDefaultAsync();
            if (updatingUser.Result == null)
                return false;
            var update = Builders<ArticleModel>.Update
                .Set(x => x.Content, article.Content)
                .Set(x=> x.Title, article.Title)
                .Set(x => x.Category, article.Category)
                .Set(x => x.CreatedUser, article.CreatedUser);
            await _repository.Articles.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<DeleteResult> Remove(string articleId)
        {
            var filter = Builders<ArticleModel>.Filter.Eq("id", articleId);
            return await _repository.Articles.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _repository.Articles.DeleteManyAsync(new BsonDocument());
        }
    }
}