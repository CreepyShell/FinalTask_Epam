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
    public class AnswerUserRepository : IAnswerUserRepository
    {
        private readonly ForumDbContext _context;
        public AnswerUserRepository(IForumDb context)
        {
            _context = (ForumDbContext)context;
        }

        public async Task<AnswerUser> AnswerQuestionAsync(string userId, string answerId)
        {
            if (await _context.AnswerUsers.FirstOrDefaultAsync(au => au.AnswerId == answerId && au.UserId == userId) != null)
                throw new ArgumentException("this user answer is already exist");

            AnswerUser answerUser = new AnswerUser() { AnswerId = answerId, UserId = userId };

            return (await _context.AnswerUsers.AddAsync(answerUser)).Entity;
        }

        public async Task<IEnumerable<AnswerUser>> GetAllAsync()
        {
            return await _context.AnswerUsers.ToListAsync();
        }

        public async Task<IEnumerable<AnswerUser>> GetByAnswerIdAsync(string answerId)
        {
            return await _context.AnswerUsers.Where(au => au.AnswerId == answerId).ToListAsync();
        }

        public async Task<IEnumerable<AnswerUser>> GetByUserIdAsync(string userId)
        {
            return await _context.AnswerUsers.Where(au => au.UserId == userId).ToListAsync();
        }

        public async Task<bool> RemoveUserAnswerAsync(string userId, string answerId)
        {
            if (await _context.AnswerUsers.FirstOrDefaultAsync(au => au.AnswerId == answerId && au.UserId == userId) == null)
                throw new ArgumentException("this user answer does not exist");

            AnswerUser answerUser = new AnswerUser() { AnswerId = answerId, UserId = userId };

            _context.AnswerUsers.Remove(answerUser);

            return true;
        }
    }
}
