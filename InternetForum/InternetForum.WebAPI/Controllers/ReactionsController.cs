using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Administrator, PremiumUser, Owner")]
    public class ReactionsController : ControllerBase
    {
        private readonly ILogger<ReactionsController> _logger;
        private readonly IReactionService _reactionService;
        public ReactionsController(IReactionService reactionService, ILogger<ReactionsController> logger)
        {
            _logger = logger;
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
            _logger.LogInformation($"{reaction.IsLike}");
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

        [HttpGet("{commentId}")]
        [Route("commentreactions")]
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
