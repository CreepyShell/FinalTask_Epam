﻿using InternetForum.DAL.DomainModels;
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
        public CommentRepository(ForumDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetAnswersToCommentByIdAsync(int commentId)
        {
            return await _context.Comments.Where(c => c.CommentId == commentId).ToListAsync();
        }

        public async Task<Comment> UpdatePostAsync(Comment newComment)
        {
            Comment comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == newComment.Id);
            if (comment == null)
                throw new ArgumentException("did not find comment with this id");
            _context.Comments.Attach(newComment);
            _context.Entry(newComment).State = EntityState.Modified;
            return newComment;
        }
    }
}
