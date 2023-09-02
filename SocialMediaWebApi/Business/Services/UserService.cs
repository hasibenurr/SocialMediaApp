using MongoDB.Bson;
using MongoDB.Driver;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Entities;
using SocialMediaWebApi.Entities.IEntities;

namespace SocialMediaWebApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _service;

        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _service = database.GetCollection<User>("users");
        }

        public User Create(User user)
        {
            _service.InsertOne(user);

            return user;
        }

        public List<User> Get()
        {
           return _service.Find(user => true).ToList();
        }

        public User GetById(string id)
        {
            return _service.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Update(string id, User user)
        {
            _service.ReplaceOne(user => user.Id == id, user);
        }

        public void Delete(string id)
        {
            _service.DeleteOne(user=>user.Id == id);
        }
    }
}
