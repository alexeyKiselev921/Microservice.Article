using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Article.Service.Models;
using MongoDB.Driver;

namespace Microservice.Article.Service.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleModel>> GetAll();
        Task<ArticleModel> Get(string articleId);
        Task Create(ArticleModel article);
        Task<bool> Update(ArticleModel article);
        Task<DeleteResult> Remove(string articleId);
        Task<DeleteResult> RemoveAll();
    }
}