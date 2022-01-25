using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class PostRepository : BaseGenericRepository<Post>, IPostRepository
    {
        public PostRepository(IForumDb context) : base(context)
        {
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
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == newPost.Id);
            if (post == null)
                throw new ArgumentException("did not find post with this id");
            _context.Posts.Attach(newPost);
            _context.Entry(newPost).State = EntityState.Modified;
            return newPost;
        }
    }
}
