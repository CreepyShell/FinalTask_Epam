using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<User> UpdateUserAsync(User updatedUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
            if (user == null)
                throw new ArgumentException("did not find user with this id");
            if (user.UserName != updatedUser.UserName)
                throw new ArgumentException("you can not change username");
            _context.Users.Attach(updatedUser);
            _context.Entry(updatedUser).State = EntityState.Modified;
            return updatedUser;
        }
    }
}
