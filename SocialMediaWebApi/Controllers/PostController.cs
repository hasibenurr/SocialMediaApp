using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Dtos;
using SocialMediaWebApi.Entities;
using System.Security.Cryptography;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<PostDto>> GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postService.Get());

            return posts;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Post> GetById(string id)
        {        
            var Post = _postService.GetById(id);
            if (Post is null)
            {
                return NotFound("Post not found");
            }

            return Post;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([FromBody] PostDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            _postService.Create(post);

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Update(string id, [FromBody]PostDto dto)
        {
            var existingPost = _postService.GetById(id);

            if (existingPost is null)
            {
                return NotFound("Post not found");
            }

            _postService.Update(id, _mapper.Map<Post>(dto));

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingPost = _postService.GetById(id);

            if (existingPost is null)
            {
                return NotFound("Post not found");
            }

            _postService.Delete(id);

            return Ok();
        }
    }
}
