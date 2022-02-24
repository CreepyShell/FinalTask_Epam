using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.WebAPI.Filters;
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
    [ApiController]
    [BaseExceptionFilter]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;
        public PostsController(IPostService postService, ILogger<PostsController> logger)
        {
            _logger = logger;
            _postService = postService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            _logger.LogInformation("someone get all posts\n");
            return Ok(await _postService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostDTO>> GetPostById(string id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<PostDTO>> UpdatePost([FromBody] PostDTO updatedPost)
        {
            _logger.LogInformation($"{updatedPost.Header} and {updatedPost.Text}");
            return Ok(await _postService.UpdateAsync(updatedPost));
        }

        [HttpDelete]
        [Route("admin/{username}/{postId}")]
        [Authorize(Roles = "Administrator, Owner")]
        public async Task<ActionResult<PostDTO>> DeletePostByUserName(string username, string postId)
        {
            PostDTO post = (await _postService.GetPostsByUsername(username)).FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return BadRequest("This user do not have posts or incorect postId");
            return Ok(await _postService.DeleteAsync(post.Id));
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<ActionResult<PostDTO>> DeleteUserPost(string postId)
        {
            string username = this.GetUsername();
            string userId = this.GetUserId();
            if (!(await _postService.GetPostsByUsername(username)).Select(p => p.UserId).Contains(userId))
                return BadRequest("User do not contrains post with this id");
            return Ok(await _postService.DeleteAsync(postId));
        }

        [HttpPost]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostDTO post)
        {
            _logger.LogInformation($"creating post:{post.PostTopic}");
            return Ok(await _postService.AddEntityAsync(post));
        }

        [HttpGet]
        [Route("bydate")]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsByDate([FromHeader] DateTime startDate, [FromHeader] DateTime endDate)
        {
            IEnumerable<PostDTO> posts = await _postService.GetPostsByDate(startDate, endDate);

            return Ok(posts);
        }

        [HttpGet]
        [Route("popular/{count}")]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPopularPosts(int count)
        {
            IEnumerable<PostDTO> posts = await _postService.GetMostPopularPosts(count);

            return Ok(posts);
        }

        [HttpGet]
        [Route("discussed/{count}")]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetDiscussedPosts(int count)
        {
            IEnumerable<PostDTO> posts = await _postService.GetMostDiscussedPosts(count);

            return Ok(posts);
        }

    }
}
