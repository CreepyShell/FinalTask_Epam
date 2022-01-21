using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<Post> UpdatePostAsync(Post newPost);
        Task ReactPostAsync(int postId, PostReaction newReaction);
        Task<IEnumerable<Post>> GetPostsWithReactionsAsync();
        Task<IEnumerable<Post>> GetPostsWithCommentsAsync();
    }
}
