using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Dtos;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userService.Get());

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(string Id)
        {        
            var user = _userService.GetById(Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return user;
        }

        [HttpPost]
        public ActionResult Create([FromBody] UserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _userService.Create(user);

            return Ok($"User {user.Id} succesfully created!");
        }

        [HttpPut("{id}")]
        public ActionResult Update(string Id, [FromBody]UserDto dto)
        {
            var existingUser = _userService.GetById(Id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            _userService.Update(Id, _mapper.Map<User>(dto));

            return Ok($"User {Id} succesfully updated!");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string Id)
        {
            var existingUser = _userService.GetById(Id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            _userService.Delete(Id);

            return Ok($"User {Id} succesfully deleted!");
        }
    }
}
