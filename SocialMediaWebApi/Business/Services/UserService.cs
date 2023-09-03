using MongoDB.Driver;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Entities;
using SocialMediaWebApi.Entities.IEntities;

namespace SocialMediaWebApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Post> _postCollection;

        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _userCollection = database.GetCollection<User>("users");
            _postCollection = database.GetCollection<Post>("posts");
        }

        public User Create(User user)
        {
            _userCollection.InsertOne(user);

            return user;
        }

        public List<User> Get()
        {
            return _userCollection.Find(user => true).ToList();
        }

        public User GetById(string id)
        {
            return _userCollection.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Update(string id, User user)
        {
            _userCollection.ReplaceOne(user => user.Id == id, user);
        }

        public async void Delete(string id)
        {
            // Begin Transaction
            using var session = await _mongoClient.StartSessionAsync();

            session.StartTransaction();
            try
            {
                /// All posts that the user have must be deleted
                var postsList = _postCollection.Find(post => post.UserId == id).ToList();
                foreach (var post in postsList)
                {
                    _postCollection.DeleteOne(post => post.Id == post.Id);
                }

                _userCollection.DeleteOne(user => user.Id == id);

                /// Made it here without error? Commit the transaction
                await session.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                await session.AbortTransactionAsync();
            }
        }
    }
}
