using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Article.Service.Models;
using Microservice.Article.Service.Resolvers;
using Microservice.Article.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservice.Article.Service.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET: api/Comment
        private readonly ICommentService _commentService;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public CommentController(ICommentService service)
        {
            _commentService = service;
            _jsonSerializerSettings = new JsonSerializerSettings() { ContractResolver = new LowercaseContractResolver() };
        }

        // GET: api/Article
        [HttpGet]
        [Route("{articleId}/comments")]
        public Task<IEnumerable<CommentModel>> Get(string articleId)
        {
            return _commentService.GetAll(articleId);
        }

        // GET: api/Article/5
        [HttpGet("{id}", Name = "GetComment")]
        [Route("{articleId}/comment/{id}")]
        public async Task<string> Get(string id, string articleId)
        {
            try
            {
                var comment = await _commentService.Get(articleId, id);
                if (comment == null)
                {
                    return JsonConvert.SerializeObject("No comment found");
                }

                return JsonConvert.SerializeObject(comment, Formatting.Indented, _jsonSerializerSettings);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e.ToString());
            }
        }

        // POST: api/Article
        [HttpPost]
        [Route("{articleId}/createComment")]
        public async Task<IActionResult> Post(string articleId, CommentModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CommentText))
                    return BadRequest("Please add comment text");
                await _commentService.Create(model);
                return Ok("Comment has been added successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // PUT: api/Article/5
        [HttpPut("{id}")]
        [Route("{articleId}/editComment/{id}")]
        public async Task<IActionResult> Put(string articleId, [FromBody]CommentModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CommentText))
                    return BadRequest("Please add comment text");
                var result = await _commentService.Update(model);
                if (result)
                {
                    return Ok("Comment has been updated successfully");
                }

                return BadRequest("No comment found to update");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Route("{articleId}/deleteComment/{id}")]
        public async Task<IActionResult> Delete(string articleId, string id)
        {
            try
            {
                await _commentService.Remove(id);
                return Ok("Comment has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("{articleId}/deleteAllComments")]
        public IActionResult DeleteAll(string articleId)
        {
            try
            {
                _commentService.RemoveAll(articleId);
                return Ok("All comments has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
