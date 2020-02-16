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
    public class CommentController : ControllerBase
    {
        // GET: api/Comment
        private readonly ICommentService _commentService;

        public CommentController(ICommentService service)
        {
            _commentService = service;
        }

        // GET: api/Article
        [HttpGet]
        public Task<IEnumerable<CommentModel>> Get()
        {
            return _commentService.GetAll();
        }

        // GET: api/Article/5
        [HttpGet("{id}", Name = "GetComment")]
        public async Task<string> Get(string id)
        {
            try
            {
                var user = await _commentService.Get(id);
                if (user == null)
                {
                    return JsonConvert.SerializeObject("No comment found");
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
        public async Task<IActionResult> Post(CommentModel model)
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
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(CommentModel model)
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
        public async Task<IActionResult> Delete(string id)
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
        [Route("deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _commentService.RemoveAll();
                return Ok("All comments has been deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
