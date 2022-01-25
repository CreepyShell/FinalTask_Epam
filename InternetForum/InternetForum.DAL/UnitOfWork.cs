using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InternetForum.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IPostRepository postRepository, IPostReactionRepository postReactionRepository, IUserRepository userRepository, 
            ICommentRepository commentRepository, ICommentReactionRepository commentReactionRepository, UserManager<AuthUser> userManager)
        {
            PostRepository = postRepository;
            UserRepostory = userRepository;
            CommentRepository = commentRepository;
            PostReactionRepository = postReactionRepository;
            CommentReactionRepository = commentReactionRepository;
            UserManager = userManager;
        }
        public IPostRepository PostRepository { get; set; }
        public IUserRepository UserRepostory { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IPostReactionRepository PostReactionRepository { get; set; }
        public ICommentReactionRepository CommentReactionRepository { get; set; }
        public UserManager<AuthUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }

        public async Task<int> SaveChangesAsync()            
        {
            return await PostRepository.SaveChanesAsync();
        }
    }
}
