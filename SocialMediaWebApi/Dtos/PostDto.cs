using MongoDB.Bson;

namespace SocialMediaWebApi.Dtos
{
    public class PostDto
    {
        public string Id { get; set; } = String.Empty;

        public string UserId { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public string Message { get; set; } = String.Empty;

        public int Category { get; set; } 
    }
}
