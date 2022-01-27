using InternetForum.BLL.ModelsDTo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IReactionService
    {
        Task<ReactionDTO> ReactToComment(ReactionDTO reaction);
        Task<ReactionDTO> ReactToPost(ReactionDTO reaction);
        Task<IEnumerable<ReactionDTO>> GetReactionsByPostId(string postId);
        Task<IEnumerable<ReactionDTO>> GetReactionsByCommentId(string commentId);
        Task<IEnumerable<ReactionDTO>> GetPostReactionsByUserId(string userId);
        Task<IEnumerable<ReactionDTO>> GetCommentReactionsByUserId(string userId);
    }
}
