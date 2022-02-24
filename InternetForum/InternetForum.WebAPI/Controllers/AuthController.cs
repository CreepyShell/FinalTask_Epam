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
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] AuthUserDTO authUser)
        {
            UserDTO createdUser = await _authService.Register(authUser, _jwtSettings);
            _logger.LogInformation($"Someone with {authUser.Username} registered");
            return Created($"api/users/{createdUser.Id}", createdUser);
        }

        [HttpPost]
        [AllowAnonymous]
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
            return Ok("{\"rezult\":\"Logged out\"}");
        }

        [HttpPut]
        [Authorize]
        [Route("changepass")]
        public async Task<ActionResult<Token>> UpdatePassword([FromHeader] string currentPassword, [FromHeader] string newPassword)
        {
           Token token = await _authService.UpdatePassword(this.GetUsername(), currentPassword, newPassword, _jwtSettings);
            _logger.LogInformation($"{this.GetUsername()} changed password");
            return Ok(token);
        }

        [HttpPut]
        [Authorize]
        [Route("changecodewords")]
        public async Task<IActionResult> ChangeCodeWords([FromBody] UserDTO user, [FromHeader] string newCodeWords)
        {
            await _authService.UpdateCodeWord(user, newCodeWords);
            return Ok();
        }
    }
}