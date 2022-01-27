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
    public class CommentRepository : BaseGenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IForumDb context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetAnswersToCommentByIdAsync(string commentId)
        {
            return await _context.Comments.Where(c => c.CommentId == commentId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId)
        {
            return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsWithReactions()
        {
            return await _context.Comments.Include(c => c.Reactions).ToListAsync();
        }

        public async Task<Comment> UpdatePostAsync(Comment newComment)
        {
            Comment comment = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == newComment.Id);
            if (comment == null)
                throw new ArgumentException("did not find comment with this id");
            comment = newComment;
            _context.Comments.Attach(comment);
            _context.Entry(comment).State = EntityState.Modified;
            return comment;
        }
    }
}
