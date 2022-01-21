using InternetForum.Administration.DAL.UserManagerRepository;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;

namespace InternetForum.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; set; }
        IUserRepostory UserRepostory { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IUserManager UserManager { get; set; }
    }
}
