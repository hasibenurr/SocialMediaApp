using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaWebApi.Security;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IdentityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Token token = TokenHandler.CreateToken(_configuration);

            return Ok(token);
        }
    }
}
