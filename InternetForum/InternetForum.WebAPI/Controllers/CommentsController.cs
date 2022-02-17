using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            IEnumerable<CommentDTO> comments = await _commentService.GetMostPopularCommentsByPostId(postId, count);

            return Ok(comments);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> UpdateComment([FromBody] CommentDTO updatedComment)
        {
            if (updatedComment.UserId != this.GetUserId())
                return Forbid();

            CommentDTO comment = await _commentService.UpdateAsync(updatedComment);

            return Ok(comment);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> DeleteComment([FromBody] CommentDTO updatedComment)
        {
            if (updatedComment.UserId != this.GetUserId())
                return Forbid();

            CommentDTO comment = await _commentService.UpdateAsync(updatedComment);

            return Ok(comment);
        }
        [HttpPut]
        [Route("admin")]
        [Authorize(Roles = "Administator, Owner")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> UpdateCommentByAdmin([FromBody] CommentDTO updatedComment)
        {
            CommentDTO comment = await _commentService.UpdateAsync(updatedComment);

            return Ok(comment);
        }

        [HttpDelete]
        [Route("admin")]
        [Authorize(Roles = "Administator, Owner")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> DeleteCommentByAdmin([FromBody] CommentDTO updatedComment)
        {
            CommentDTO comment = await _commentService.UpdateAsync(updatedComment);

            return Ok(comment);
        }
    }
}
