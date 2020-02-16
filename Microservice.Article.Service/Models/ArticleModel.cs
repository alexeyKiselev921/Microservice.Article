using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.Article.Service.Models
{
    public class ArticleModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedUser { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}