using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Repositories
{
    public class UserRepository : BaseGenericRepository<User>, IUserRepository
    {
        public UserRepository(IForumDb context) : base(context)
        {

        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
           User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                throw new ArgumentException("did not find user with this username");
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersWithReacions()
        {
            return await _context.Users.Include(u => u.PostReactions).Include(u => u.CommentReactions).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserWithComments()
        {
            return await _context.Users.Include(u => u.Comments).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserWithPosts()
        {
            return await _context.Users.Include(u => u.Posts).ToListAsync();
        }

        public  async Task<IEnumerable<User>> GetUserWithQuestionnaires()
        {
            return await _context.Users.Include(u => u.Questionnaires).ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User updatedUser)
        {
            User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
            if (user == null)
                throw new ArgumentException("did not find user with this id");
            if (user.UserName != updatedUser.UserName)
                throw new ArgumentException("you can not change username");
            user = updatedUser;
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }
    }
}
