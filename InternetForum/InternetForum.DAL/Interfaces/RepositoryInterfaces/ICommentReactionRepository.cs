using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface ICommentReactionRepository : IBaseRepository<CommentReaction>
    {
        Task<CommentReaction> UpdateAsync(CommentReaction reaction);
        Task<IEnumerable<CommentReaction>> GetCommentReactionsByCommentId(string commentId);
    }
}
