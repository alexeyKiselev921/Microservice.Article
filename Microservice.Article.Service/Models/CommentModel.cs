using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.Article.Service.Models
{
    public class CommentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CommentText { get; set; }
        public string Author { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ArticleId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}