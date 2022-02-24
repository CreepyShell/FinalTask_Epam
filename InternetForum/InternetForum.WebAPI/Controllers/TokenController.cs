using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenExceptionFilter]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenService tokenService, IOptionsSnapshot<JwtSettings> snapshot, ILogger<TokenController> logger)
        {
            _jwtSettings = snapshot.Value;
            _tokenService = tokenService;
            _logger = logger;
        }
        [HttpPut]
        [Route("refresh")]
        public async Task<ActionResult<Token>> RefreshToken([FromHeader] string accessToken,[FromHeader] string refreshToken)
        {
            Token token = await _tokenService.RefreshTokenAsync(accessToken, refreshToken, _jwtSettings);
            _logger.LogInformation("token was updated");
            return Ok(token);
        }

        [HttpPost]
        [Route("new")]
        [Authorize]
        public async Task<ActionResult<Token>> GetNewToken()
        {
            string userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            _logger.LogInformation($"{userName} generated new token");
            return await _tokenService.GenerateTokenAsync(userName, _jwtSettings);
        }
    }
}
