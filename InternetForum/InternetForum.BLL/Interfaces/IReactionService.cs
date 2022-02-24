using InternetForum.BLL.ModelsDTo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IReactionService
    {
        /// <summary>
        /// React to comment: if comment has reaction with given userId and new reaction is same, delete reaction
        /// if comment has reaction and new reaction is not same, update reaction to new reaction
        /// if comment do not have reaction, add it
        /// </summary>
        /// <param name="reaction">New reaction</param>
        /// <returns>If comment has reaction with userId and new reaction is same, null
        /// else updated or created reaction</returns>
        /// <exception cref="ArgumentNullException">If new reaction is null</exception>
        /// <exception cref="InvalidOperationException">If new reaction is invalid</exception>
        Task<ReactionDTO> ReactToComment(ReactionDTO reaction);
        /// <summary>
        /// React to post: if post has reaction with given userId and new reaction is same, delete reaction
        /// if post has reaction and new reaction is not same, update reaction to new reaction
        /// if post do not have reaction, add it
        /// </summary>
        /// <param name="reaction">New reaction</param>
        /// <returns>If post has reaction with userId and new reaction is same, null
        /// else updated or created reaction</returns>
        /// <exception cref="ArgumentNullException">If new reaction is null</exception>
        /// <exception cref="InvalidOperationException">If new reaction is invalid</exception>
        Task<ReactionDTO> ReactToPost(ReactionDTO reaction);
        Task<IEnumerable<ReactionDTO>> GetReactionsByPostId(string postId);
        Task<IEnumerable<ReactionDTO>> GetReactionsByCommentId(string commentId);
        Task<IEnumerable<ReactionDTO>> GetPostReactionsByUserId(string userId);
        Task<IEnumerable<ReactionDTO>> GetCommentReactionsByUserId(string userId);
    }
}
