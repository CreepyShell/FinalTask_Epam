using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseExceptionFilter]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentsController> _logger;
        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }
        [HttpPost]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CommentDTO newComment)
        {
            CommentDTO comment = await _commentService.AddEntityAsync(newComment);

            return Created($"/api/comments/{comment.Id}", comment);
        }

        [HttpPost("{commentId}")]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<CommentDTO>> AddCommentToComment(string commentId, [FromBody] CommentDTO newComment)
        {
            CommentDTO comment = await _commentService.CreateCommentToCommentAsync(newComment, commentId);

            return Created($"/api/comments/{comment.Id}", comment);
        }

        [HttpGet("{postId}/{count}")]
        [Authorize]
        [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByPostId(string postId, int count)
        {
            IEnumerable<CommentDTO> comments = (await _commentService.GetCommentsByPostId(postId)).Take(count).ToArray();

            return Ok(comments);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<CommentDTO>> UpdateComment([FromBody] CommentDTO updatedComment)
        {
            if (updatedComment.UserId != this.GetUserId())
                return Forbid();

            CommentDTO comment = await _commentService.UpdateAsync(updatedComment);

            return Ok(comment);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromBody] CommentDTO deletedComment)
        {
            if (deletedComment.UserId != this.GetUserId())
                return Forbid();

            bool rez = await _commentService.DeleteAsync(deletedComment.Id);
            if (!rez)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("admin/{userId}")]
        [Authorize(Roles = "Administator, Owner")]
        public async Task<IActionResult> DeleteCommentByAdmin(string userId, [FromBody] CommentDTO deletedComments)
        {
            CommentDTO comment = (await _commentService.GetCommentsByUserId(userId)).FirstOrDefault(c => c.Id == deletedComments.Id);

            if (comment == null)
                return BadRequest("This user do not have comment with given id"); 

            bool rez = await _commentService.DeleteAsync(deletedComments.Id);

            return Ok(rez);
        }
    }
}
