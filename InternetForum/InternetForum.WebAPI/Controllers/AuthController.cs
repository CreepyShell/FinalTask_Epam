using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [AuthExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, IOptionsSnapshot<JwtSettings> snapshot, ILogger<AuthController> logger)
        {
            _authService = authService;
            _jwtSettings = snapshot.Value;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] AuthUserDTO authUser)
        {
            UserDTO createdUser = await _authService.Register(authUser, _jwtSettings);
            return Created($"api/users/{createdUser.Id}", createdUser);
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<UserDTO>> LogInUser([FromBody] AuthUserDTO authUser)
        {
            UserDTO loginUser = await _authService.LogIn(authUser, _jwtSettings);
            _logger.LogInformation($"{loginUser.UserName} was loggined");
            return Ok(loginUser);
        }

        [HttpPut]
        [Route("logOut")]
        public async Task<IActionResult> LogOut([FromBody] UserDTO user)
        {
            await _authService.LogOut(user);
            _logger.LogInformation($"{user.UserName} logged out");
            return Ok("Logged out");
        }
    }
}
