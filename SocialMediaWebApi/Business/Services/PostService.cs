using MongoDB.Driver;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Entities;
using SocialMediaWebApi.Entities.IEntities;

namespace SocialMediaWebApi.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _service;

        public PostService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _service = database.GetCollection<Post>("posts");
        }

        public Post Create(Post Post)
        {
            _service.InsertOne(Post);

            return Post;
        }

        public List<Post> Get()
        {
            return _service.Find(Post => true).ToList();
        }

        public Post GetById(string id)
        {
            return _service.Find(Post => Post.Id == id).FirstOrDefault();
        }

        public void Update(string id, Post Post)
        {
            _service.ReplaceOne(Post => Post.Id == id, Post);
        }

        public void Delete(string id)
        {
            _service.DeleteOne(Post => Post.Id == id);
        }
    }
}
