using AutoMapper;
using SocialMediaWebApi.Dtos;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
