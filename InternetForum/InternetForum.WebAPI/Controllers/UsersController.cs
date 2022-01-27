using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
       public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult< IEnumerable<UserDTO>>> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }
        [Authorize(Roles = "User"), Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string username)
        {
            return Ok(await _userService.GetUserByNameAsync(username));
        }

        // POST api/<UsersController>
        [HttpPost]
        [Authorize(Roles = "PremiumUser"), Authorize(Roles = "Administrator")]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO user)
        {
            return await _userService.AddEntityAsync(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserDTO user)
        {
            user.Id = this.GetUserId();
            return await _userService.UpdateAsync(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> DeleteUserById([FromBody] UserDTO user)
        {
            if (user.Id != this.GetUserId())
                return Unauthorized();
            return await _userService.DeleteAsync(user.Id);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("adminId/{username}")]
        public async Task<ActionResult<bool>> DeleteUserById(string username)
        {
            return await _userService.DeleteUserByNameAsync(username);
        }
    }
}
