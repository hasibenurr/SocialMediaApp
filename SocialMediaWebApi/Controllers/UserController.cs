using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            #region MongoDB Connection Settings
            //var dbHost = "localhost";
            //var dbName = "dms_user";
            //var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            //var mongoUrl = MongoUrl.Create(connectionString);
            //var mongoClient = new MongoClient(mongoUrl);
            //var dataBase = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            //_userCollection = dataBase.GetCollection<User>("user");
            #endregion

            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return _userService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(string userId)
        {        
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return user;
        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            _userService.Create(user);

            return Ok($"User {user.Id} succesfully created!");
        }

        [HttpPut("{id}")]
        public ActionResult Update(string userId, [FromBody]User user)
        {
            var existingUser = _userService.GetById(userId);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            _userService.Update(userId, user);

            return Ok($"User {user.Id} succesfully updated!");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string userId)
        {
            var existingUser = _userService.GetById(userId);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            _userService.Delete(userId);

            return Ok($"User {userId} succesfully deleted!");
        }
    }
}
