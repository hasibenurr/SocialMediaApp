using MongoDB.Bson;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string Surname { get; set; } = String.Empty;

        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
