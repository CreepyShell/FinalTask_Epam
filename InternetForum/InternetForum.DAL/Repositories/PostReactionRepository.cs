using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class PostReactionRepository : BaseGenericRepository<PostReaction>, IPostReactionRepository
    {
        public PostReactionRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<PostReaction>> GetPostReactionsByPostId(string postId)
        {
            return await _context.PostReactions.Where(pr => pr.PostId == postId).ToListAsync();
        }

        public async Task<PostReaction> UpdateAsync(PostReaction reaction)
        {
            PostReaction postReaction = await _context.PostReactions.AsNoTracking().FirstOrDefaultAsync(pr => pr.Id == reaction.Id);
            if (postReaction == null)
                throw new ArgumentException("did not find post with this id");
            postReaction = reaction;
            _context.PostReactions.Attach(postReaction);
            _context.Entry(postReaction).State = EntityState.Modified;
            return postReaction;
        }
    }
}
