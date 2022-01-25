using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;
        public AuthController(IAuthService authService, IOptionsSnapshot<JwtSettings> snapshot)
        {
            _authService = authService;
            _jwtSettings = snapshot.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] AuthUserDTO authUser)
        {
            return await _authService.Register(authUser, _jwtSettings);
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<UserDTO>> LogInUser([FromBody] AuthUserDTO authUser)
        {
            return await _authService.LogIn(authUser, _jwtSettings);
        }
    }
}
