using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialMediaWebApi.Entities
{
    [Serializable, BsonIgnoreExtraElements]
    public class User
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; } = String.Empty;

        [BsonElement("surname"), BsonRepresentation(BsonType.String)]
        public string SurName { get; set; } = String.Empty;

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; } = String.Empty;

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; } = String.Empty;

        /// <summary>
        /// Relation with Post table
        /// </summary>
        [BsonElement("posts")]
        public List<Post> Posts { get; set; } = new List<Post>(); 
    }
}
