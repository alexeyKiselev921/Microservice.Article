using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Article.Service.Models;
using Microservice.Article.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservice.Article.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService service)
        {
            _articleService = service;
        }

        // GET: api/Article
        [HttpGet]
        public Task<IEnumerable<ArticleModel>> Get()
        {
            return _articleService.GetAll();
        }

        // GET: api/Article/5
        [HttpGet("{id}", Name = "GetArticle")]
        public async Task<string> Get(string id)
        {
            try
            {
                var user = await _articleService.Get(id);
                if (user == null)
                {
                    return JsonConvert.SerializeObject("No article found");
                }

                return JsonConvert.SerializeObject(user);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e.ToString());
            }
        }

        // POST: api/Article
        [HttpPost]
        public async Task<IActionResult> Post(ArticleModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Content))
                    return BadRequest("Please content");
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest("Please add title");
                if (string.IsNullOrEmpty(model.Category))
                    return BadRequest("Please category");

                model.CreatedDate = DateTime.Now;
                await _articleService.Create(model);
                return Ok("Article has been added successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // PUT: api/Article/5
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(ArticleModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Content))
                    return BadRequest("Please content");
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest("Please add title");
                if (string.IsNullOrEmpty(model.Category))
                    return BadRequest("Please category");
                var result = await _articleService.Update(model);
                if (result)
                {
                    return Ok("Article has been updated successfully");
                }

                return BadRequest("No article found to update");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _articleService.Remove(id);
                return Ok("Article has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _articleService.RemoveAll();
                return Ok("All articles has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
