using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SocialMediaWebApi.Entities
{
    public class Post
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("message")]
        public string Message { get; set; } = String.Empty;
    }
}
