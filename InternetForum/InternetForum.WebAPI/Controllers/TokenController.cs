using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public TokenController(ITokenService tokenService, IOptionsSnapshot<JwtSettings> snapshot)
        {
            _jwtSettings = snapshot.Value;
            _tokenService = tokenService;
        }
        [HttpPut]
        [Route("refresh")]
        public async Task<ActionResult<Token>> RefreshToken([FromHeader] string accessToken,[FromHeader] string refreshToken)
        {
            return await _tokenService.RefreshTokenAsync(accessToken, refreshToken, _jwtSettings);
        }

        [HttpPost]
        [Route("new")]
        [Authorize]
        public async Task<ActionResult<Token>> GetNewToken()
        {
            string userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            return await _tokenService.GenerateTokenAsync(userName, _jwtSettings);
        }
    }
}
