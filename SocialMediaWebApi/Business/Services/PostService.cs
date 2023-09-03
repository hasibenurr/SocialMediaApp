using MongoDB.Bson;
using MongoDB.Driver;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Entities;
using SocialMediaWebApi.Entities.IEntities;

namespace SocialMediaWebApi.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Post> _postCollection;
        private readonly IUserService _userService;

        public PostService(IDatabaseSettings settings, IMongoClient mongoClient, IUserService userService)
        {
            _mongoClient = mongoClient;
            _userService = userService;

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _postCollection = database.GetCollection<Post>("posts");
        }

        public List<Post> Get()
        {
            return _postCollection.Find(Post => true).ToList();
        }

        public Post GetById(string id)
        {
            return _postCollection.Find(Post => Post.Id == id).FirstOrDefault();
        }

        public Post Create(Post Post)
        {
            // Begin Transaction
            using var session = _mongoClient.StartSession();

            session.StartTransaction();
            try
            {
                _postCollection.InsertOne(Post);

                /// Users post list should be updated
                var user = _userService.GetById(Post.UserId);
                user.Posts.Add(Post);

                _userService.Update(Post.UserId, user);

                /// Made it here without error? Commit the transaction
                session.CommitTransaction();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }

            return Post;
        }

        public void Update(string id, Post modifiedPost)
        {
            // Begin Transaction
            using var session = _mongoClient.StartSession();

            session.StartTransaction();
            try
            {
                /// Users post list should be updated
                List<Post> userPosts = new List<Post>();

                var user = _userService.GetById(modifiedPost.UserId);

                foreach (var item in user.Posts)
                {
                    userPosts.Add(item);
                }

                var oldPost = userPosts.Find(p => p.Id == id && p.UserId == modifiedPost.UserId);
                if (oldPost is not null)
                {
                    var indexOfPostToUpdate = userPosts.IndexOf(oldPost);
                    user.Posts[indexOfPostToUpdate] = modifiedPost;

                    _userService.Update(modifiedPost.UserId, user);
                }

                _postCollection.ReplaceOne(Post => Post.Id == id, modifiedPost);

                /// Made it here without error? Commit the transaction
                session.CommitTransaction();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }
        }

        public void Delete(string id)
        {
            // Begin Transaction
            using var session = _mongoClient.StartSession();

            session.StartTransaction();
            try
            {
                List<Post> userPosts = new List<Post>();
                var deletedPost = GetById(id);

                /// Users post list should be deleted
                var user = _userService.GetById(deletedPost.UserId);

                foreach (var item in user.Posts)
                {
                    userPosts.Add(item);
                }

                var currentPost = userPosts.Find(p => p.Id == id && p.UserId == deletedPost.UserId);
                if (currentPost is not null)
                {
                    user.Posts.Remove(currentPost);

                    _userService.Update(deletedPost.UserId, user);
                }

                _postCollection.DeleteOne(Post => Post.Id == id);

                /// Made it here without error? Commit the transaction
                session.CommitTransaction();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }
        }
    }
}
