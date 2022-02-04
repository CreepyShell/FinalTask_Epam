using InternetForum.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRoleService _roleService;
        public RolesController(ILogger<RolesController> logger, IRoleService roleService)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        [Route("banned")]
        public async Task<ActionResult<IEnumerable<string>>> GetBannedUsers()
        {
            return Ok(await _roleService.GetUsersInRole("BannedUser"));
        }

        [HttpGet]
        [Route("premium")]
        public async Task<ActionResult<IEnumerable<string>>> GetPremiumUsers()
        {
            return Ok(await _roleService.GetUsersInRole("PremiumUser"));
        }
        [HttpPut]
        [Route("ban/{username}")]
        public async Task<IActionResult> BanUser(string username)
        {
            if (await _roleService.RemoveUserFromRole(username, "User"))
                if (await _roleService.AssignUserToRole(username, "BannedUser"))
                    return Ok();
            return NotFound("User already banned");
        }

        [HttpPut]
        [Route("unban/{username}")]
        public async Task<IActionResult> UnbanUser(string username)
        {
            if (await _roleService.RemoveUserFromRole(username, "BannedUser"))
                if (await _roleService.AssignUserToRole(username, "User"))
                    return Ok();
            return NotFound("User already unbanned");
        }
        [HttpPut]
        [Route("getpremium/{username}")]
        public async Task<IActionResult> GetPremiumUser(string username)
        {
            if (await _roleService.AssignUserToRole(username, "PremiumUser"))
                return Ok();
            return NotFound("User already premium");
        }

        [HttpPut]
        [Route("removepremium/{username}")]
        public async Task<IActionResult> RemovePremiumUser(string username)
        {
            if (await _roleService.RemoveUserFromRole(username, "PremiumUser"))
                return Ok();
            return NotFound("User already premium");
        }
        [HttpGet]
        [Route("userroles")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles()
        {
            return Ok(await _roleService.GetUserRoles(this.GetUsername()));
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("asignRole")]
        public async Task<ActionResult<IEnumerable<string>>> AsignUserToRole([FromQuery] string role, string username)
        {
            return Ok(await _roleService.AssignUserToRole(username, role));
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("removeRole")]
        public async Task<ActionResult<IEnumerable<string>>> RemoveUserFromRole([FromQuery] string role, string username)
        {
            return Ok(await _roleService.RemoveUserFromRole(username, role));
        }
    }
}
