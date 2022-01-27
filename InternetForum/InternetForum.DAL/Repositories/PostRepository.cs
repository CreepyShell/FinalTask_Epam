using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class PostRepository : BaseGenericRepository<Post>, IPostRepository
    {
        public PostRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsWithCommentsAsync()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsWithReactionsAsync()
        {
            return await _context.Posts.Include(p => p.Reactions).ToListAsync();
        }

        public async Task<Post> UpdatePostAsync(Post newPost)
        {
            Post post = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == newPost.Id);
            if (post == null)
                throw new ArgumentException("did not find post with this id");
            post = newPost;
            _context.Posts.Attach(post);
            _context.Entry(post).State = EntityState.Modified;

            return post;
        }
    }
}
