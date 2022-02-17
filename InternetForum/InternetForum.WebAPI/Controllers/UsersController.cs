using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace InternetForum.WebAPI.Controllers
{
    [UserExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet]
        [Route("userinfo/{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserInfoById(string userId)
        {
            UserDTO user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [Authorize(Roles = "Administrator"), Authorize(Roles = "Owner")]
        [HttpGet("userinfo/admin/{id}")]
        public async Task<ActionResult<UserDTO>> GetFullUserInfoById(string id)
        {
            UserDTO user = await _userService.GetFullUserInfoByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string username)
        {
            UserDTO user = await _userService.GetUserByNameAsync(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserDTO user)
        {
            _logger.LogInformation($"try to update:{user.FullName} {user.Email} {user.FullName}");
            if (user.Id != this.GetUserId())
                return Forbid();
            return Ok(await _userService.UpdateAsync(user));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserById([FromBody] UserDTO user)
        {
            if (user.Id != this.GetUserId())
                return Forbid();
            return Ok(await _userService.DeleteAsync(user.Id));
        }
        [Authorize]
        [HttpGet]
        [Route("fromtoken")]
        public async Task<ActionResult<UserDTO>> GetUserFromToken()
        {
            string userId = this.GetUserId();
            UserDTO user = await _userService.GetFullUserInfoByIdAsync(userId);
            return Ok(user);
        }
    }
}
