using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IPostReactionRepository : IBaseRepository<PostReaction>
    {
        Task<PostReaction> UpdateAsync(PostReaction reaction);
        Task<IEnumerable<PostReaction>> GetPostReactionsByPostId(string postId);
    }
}
