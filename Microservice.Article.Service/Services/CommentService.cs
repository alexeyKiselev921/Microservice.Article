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
    public class CommentService : ICommentService
    {
        private readonly MongoRepository _repository = null;

        public CommentService(IOptions<CommentDbContext> settings)
        {
            _repository = new MongoRepository(null, settings);
        }

        public async Task<IEnumerable<CommentModel>> GetAll()
        {
            return await _repository.Comments.Find(x => true).ToListAsync();
        }

        public async Task<CommentModel> Get(string commentId)
        {
            var filter = Builders<CommentModel>.Filter.Eq("id", commentId);
            return await _repository.Comments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(CommentModel comment)
        {
            await _repository.Comments.InsertOneAsync(comment);
        }

        public async Task<bool> Update(CommentModel comment)
        {
            var filter = Builders<CommentModel>.Filter.Eq("id", comment.Id);
            var updatingUser = _repository.Comments.Find(filter).FirstOrDefaultAsync();
            if (updatingUser.Result == null)
                return false;
            var update = Builders<CommentModel>.Update
                .Set(x => x.CommentText, comment.CommentText)
                .Set(x => x.CommentDate, comment.CommentDate)
                .Set(x => x.Author, comment.Author);
            await _repository.Comments.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<DeleteResult> Remove(string commentId)
        {
            var filter = Builders<ArticleModel>.Filter.Eq("id", commentId);
            return await _repository.Articles.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _repository.Articles.DeleteManyAsync(new BsonDocument()); ;
        }
    }
}