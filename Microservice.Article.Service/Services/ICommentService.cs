using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Article.Service.Models;
using MongoDB.Driver;

namespace Microservice.Article.Service.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetAll();
        Task<CommentModel> Get(string commentId);
        Task Create(CommentModel comment);
        Task<bool> Update(CommentModel comment);
        Task<DeleteResult> Remove(string commentId);
        Task<DeleteResult> RemoveAll();
    }
}