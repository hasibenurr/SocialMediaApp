using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SocialMediaWebApi.Entities
{
    public class Post
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_id"), BsonRepresentation(BsonType.Int32)]
        public int UserId { get; set; }

        [BsonElement("message"), BsonRepresentation(BsonType.String)]
        public string Message { get; set; } = String.Empty;
    }
}
