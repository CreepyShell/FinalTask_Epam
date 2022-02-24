using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseExceptionFilter]
    [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
    public class ReactionsController : ControllerBase
    {
        private readonly IReactionService _reactionService;
        public ReactionsController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPut]
        [Route("comment")]
        public async Task<ActionResult<ReactionDTO>> ReactToComment([FromBody] ReactionDTO reaction)
        {
            ReactionDTO newReaction = await _reactionService.ReactToComment(reaction);

            return Ok(newReaction);
        }

        [HttpPut]
        [Route("post")]
        public async Task<ActionResult<ReactionDTO>> ReactToPost([FromBody] ReactionDTO reaction)
        {
            ReactionDTO newReaction = await _reactionService.ReactToPost(reaction);

            return Ok(newReaction);
        }
        [HttpGet]
        [Route("postreactions/{postId}")]
        public async Task<ActionResult<IEnumerable<ReactionDTO>>> GetReactionsByPostId(string postId)
        {
            IEnumerable<ReactionDTO> reactions = await _reactionService.GetReactionsByPostId(postId);

            return Ok(reactions);
        }

        [HttpGet]
        [Route("commentreactions/{commentId}")]
        public async Task<ActionResult<IEnumerable<ReactionDTO>>> GetReactionsByCommentId(string commentId)
        {
            IEnumerable<ReactionDTO> reactions = await _reactionService.GetReactionsByCommentId(commentId);

            return Ok(reactions);
        }
        [HttpGet]
        [Route("userpostlikes")]
        public async Task<ActionResult<IEnumerable<ReactionDTO>>> GetPostReactionsByUserId()
        {
            string userId = this.GetUserId();

            IEnumerable<ReactionDTO> reactions = await _reactionService.GetPostReactionsByUserId(userId);

            return Ok(reactions);
        }

        [HttpGet]
        [Route("usercommentlikes")]
        public async Task<ActionResult<IEnumerable<ReactionDTO>>> GetCommentReactionsByUserId()
        {
            string userId = this.GetUserId();

            IEnumerable<ReactionDTO> reactions = await _reactionService.GetReactionsByCommentId(userId);

            return Ok(reactions);
        }
    }
}
