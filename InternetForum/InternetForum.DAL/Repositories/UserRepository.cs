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
    public class UserRepository : BaseGenericRepository<User>, IUserRepository
    {
        public UserRepository(IForumDb context) : base(context)
        {

        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
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

        public async Task<bool> RemoveUserAndUserDataAsync(string id)
        {
            User deletedUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (deletedUser == null)
                throw new ArgumentException("did not find user with this id");
            if (!await DeleteAsync(deletedUser))
                return false;

            _context.AnswerUsers.RemoveRange(await _context.AnswerUsers.AsNoTracking().Where(au => au.UserId == id).ToListAsync());
            _context.PostReactions.RemoveRange(await _context.PostReactions.AsNoTracking().Where(pr => pr.UserId == id).ToListAsync());
            _context.CommentReactions.RemoveRange(await _context.CommentReactions.AsNoTracking().Where(cr => cr.UserId == id).ToListAsync());
            _context.Comments.RemoveRange(await _context.Comments.AsNoTracking().Where(c => c.UserId == id).ToListAsync());

            return true;
        }

        public async Task<User> UpdateUserAsync(User updatedUser)
        {
            if (updatedUser == null)
                throw new ArgumentNullException("updatedUser", "updated user is null");
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
