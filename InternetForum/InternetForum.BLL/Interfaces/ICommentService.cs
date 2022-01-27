using InternetForum.BLL.ModelsDTo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface ICommentService : ICrud<CommentDTO>
    {
        Task<CommentDTO> CreateCommetToCommentAsync(CommentDTO comment, string commentId);
        Task<IEnumerable<CommentDTO>> GetMostPopularComments(int count);
        Task<IEnumerable<CommentDTO>> GetMostPopularCommentsByPostId(string postId, int count);
    }
}
