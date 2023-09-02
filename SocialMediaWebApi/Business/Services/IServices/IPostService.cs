using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Business.Services.IServices
{
    public interface IPostService
    {
        List<Post> Get();
        Post GetById(string id);
        Post Create(Post Post);
        void Update(string id, Post Post);
        void Delete(string id);
    }
}
