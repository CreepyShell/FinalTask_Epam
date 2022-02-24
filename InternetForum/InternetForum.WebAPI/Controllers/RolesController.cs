using InternetForum.BLL.Interfaces;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [RoleExceptionFilter]
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
        [Authorize(Roles = "Administrator, Owner")]
        [Route("banned")]
        public async Task<ActionResult<IEnumerable<string>>> GetBannedUsers()
        {
            return Ok(await _roleService.GetUsersInRole("BannedUser"));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Owner")]
        [Route("premium")]
        public async Task<ActionResult<IEnumerable<string>>> GetPremiumUsers()
        {
            return Ok(await _roleService.GetUsersInRole("PremiumUser"));
        }
        [HttpPut]
        [Authorize(Roles = "Administrator, Owner")]
        [Route("ban/{username}")]
        public async Task<IActionResult> BanUser(string username)
        {
            _logger.LogInformation($"admin {this.GetUsername()} banned {username}");
            if (await _roleService.RemoveUserFromRole(username, "User"))
                if (await _roleService.AssignUserToRole(username, "BannedUser"))
                    return Ok();
            return NotFound("User already banned");
        }

        [HttpPut]
        [Authorize(Roles = "Administrator, Owner")]
        [Route("unban/{username}")]
        public async Task<IActionResult> UnbanUser(string username)
        {
            _logger.LogInformation($"admin {this.GetUsername()} unban {username}");
            if (await _roleService.RemoveUserFromRole(username, "BannedUser"))
                if (await _roleService.AssignUserToRole(username, "User"))
                    return Ok();
            return NotFound("User already unbanned");
        }
        [HttpPut]
        [Authorize(Roles = "Administrator, Owner")]
        [Route("getpremium/{username}")]
        public async Task<IActionResult> GetPremiumUser(string username)
        {
            _logger.LogInformation($"admin {this.GetUsername()} gave premium {username}");
            if (await _roleService.AssignUserToRole(username, "PremiumUser"))
                return Ok();
            return NotFound("User already premium");
        }

        [HttpPut]
        [Authorize(Roles = "Administrator, Owner")]
        [Route("removepremium/{username}")]
        public async Task<IActionResult> RemovePremiumUser(string username)
        {
            _logger.LogInformation($"admin {this.GetUsername()} took premium {username}");
            if (await _roleService.RemoveUserFromRole(username, "PremiumUser"))
                return Ok();
            return NotFound("User already premium");
        }
        [HttpGet]
        [Authorize(Roles = "Owner")]
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
            _logger.LogInformation($"Owner assinged {username} to role {role}");
            return Ok(await _roleService.AssignUserToRole(username, role));
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("removeRole")]
        public async Task<ActionResult<IEnumerable<string>>> RemoveUserFromRole([FromQuery] string role, string username)
        {
            _logger.LogInformation($"Owner removed {username} to role {role}");
            return Ok(await _roleService.RemoveUserFromRole(username, role));
        }
    }
}
