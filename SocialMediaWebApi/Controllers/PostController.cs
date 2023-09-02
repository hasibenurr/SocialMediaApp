using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaWebApi.Business.Services.IServices;
using SocialMediaWebApi.Dtos;
using SocialMediaWebApi.Entities;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public ActionResult<List<PostDto>> GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postService.Get());

            return posts;
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetById(string Id)
        {        
            var Post = _postService.GetById(Id);
            if (Post == null)
            {
                return NotFound("Post not found");
            }

            return Post;
        }

        [HttpPost]
        public ActionResult Create([FromBody] PostDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            _postService.Create(post);

            return Ok($"Post {post.Id} succesfully created!");
        }

        [HttpPut("{id}")]
        public ActionResult Update(string Id, [FromBody]PostDto dto)
        {
            var existingPost = _postService.GetById(Id);

            if (existingPost == null)
            {
                return NotFound("Post not found");
            }

            _postService.Update(Id, _mapper.Map<Post>(dto));

            return Ok($"Post {Id} succesfully updated!");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string Id)
        {
            var existingPost = _postService.GetById(Id);

            if (existingPost == null)
            {
                return NotFound("Post not found");
            }

            _postService.Delete(Id);

            return Ok($"Post {Id} succesfully deleted!");
        }
    }
}
