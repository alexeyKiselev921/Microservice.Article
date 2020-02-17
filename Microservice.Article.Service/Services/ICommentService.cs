using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Article.Service.Models;
using MongoDB.Driver;

namespace Microservice.Article.Service.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetAll(string articleId);
        Task<CommentModel> Get(string articleId, string commentId);
        Task Create(CommentModel comment);
        Task<bool> Update(CommentModel comment);
        Task<DeleteResult> Remove(string commentId);
        Task<DeleteResult> RemoveAll(string articleId);
    }
}