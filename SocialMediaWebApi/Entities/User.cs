using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApi.Entities
{
    [Serializable, BsonIgnoreExtraElements]
    public class User
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [BsonElement("surname"), BsonRepresentation(BsonType.String)]
        public string SurName { get; set; } = String.Empty;

        [Required]
        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; } = String.Empty;

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; } = String.Empty;

        [BsonElement("posts")]
        [BsonIgnoreIfNull]
        public List<Post> Posts { get; set; } = null!;
    }
}
