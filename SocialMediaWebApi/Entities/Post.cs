using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SocialMediaWebApi.Entities
{
    public class Post
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("user_id")]
        public string UserId { get; set; } = String.Empty;  

        [BsonElement("message")]
        public string Message { get; set; } = String.Empty;

    }
}
