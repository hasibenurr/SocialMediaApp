using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string SurName { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
