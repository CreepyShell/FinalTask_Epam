using InternetForum.Administration.DAL;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; set; }
        IUserRepository UserRepostory { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IPostReactionRepository PostReactionRepository { get; set; }
        ICommentReactionRepository CommentReactionRepository { get; set; }
        UserManager<AuthUser> UserManager { get; set; }
        Task<int> SaveChangesAsync();
    }
}
