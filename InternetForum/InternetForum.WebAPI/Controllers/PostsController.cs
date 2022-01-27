using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IRoleService _roleService;

        public PostsController(IRoleService roleService, IPostService postService)
        {
            _roleService = roleService;
            _postService = postService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            return Ok(await _postService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PostDTO>> GetAllPostById(string id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostDTO>> UpdatePost([FromBody] PostDTO updatedPost)
        {
            return Ok(await _postService.UpdateAsync(updatedPost));
        }

        [HttpDelete]
        [Route("admin/{username}/{postId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PostDTO>> DeletePostByUserName(string username, string postId)
        {
            PostDTO post = (await _postService.GetPostsByUsername(username)).FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return BadRequest("This user do not have posts or incorect postId");
            return Ok(await _postService.DeleteAsync(post.Id));
        }

        [HttpDelete("{postId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostDTO>> DeleteUserPost(string postId)
        {
            string username = this.GetUsername();
            string userId = this.GetUserId();
            if (!(await _postService.GetPostsByUsername(username)).Select(p => p.UserId).Contains(userId))
                return BadRequest("User do not contrains post with this id");
            return Ok(await _postService.DeleteAsync(postId));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostDTO post)
        {
            return Ok(await _postService.AddEntityAsync(post));
        }
    }
}
