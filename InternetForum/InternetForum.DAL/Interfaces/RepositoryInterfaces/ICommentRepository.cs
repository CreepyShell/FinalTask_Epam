﻿using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<Comment> UpdatePostAsync(Comment newComment);
        Task<IEnumerable<Comment>> GetAnswersToCommentByIdAsync(string commentId);
    }
}
