using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<Comment> UpdatePostAsync(Comment newComment);
        Task ReactCommentAsync(int postId, CommentReaction newReaction);
        Task<IEnumerable<Comment>> GetAnswersToCommentByIdAsync(int id);
    }
}
