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
    public class CommentReactionRepository : BaseGenericRepository<CommentReaction>, ICommentReactionRepository
    {
        public CommentReactionRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<CommentReaction>> GetCommentReactionsByCommentId(string commentId)
        {
            return await _context.CommentReactions.Where(cr => cr.CommentId == commentId).ToListAsync();
        }

        public async Task<CommentReaction> UpdateAsync(CommentReaction reaction)
        {
            CommentReaction commentReaction = await _context.CommentReactions.FirstOrDefaultAsync(cr => cr.Id == reaction.Id);
            if (commentReaction == null)
                throw new ArgumentException("did not find post with this id");
            _context.CommentReactions.Attach(reaction);
            _context.Entry(reaction).State = EntityState.Modified;
            return reaction;
        }
    }
}
