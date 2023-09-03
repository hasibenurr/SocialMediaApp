using MongoDB.Bson;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Business.Services.IServices
{
    public interface IUserService
    {
        List<User> Get();
        User GetById(string id);
        User Create(User user);
        void Update(string id, User user);
        void Delete(string id);
    }
}
