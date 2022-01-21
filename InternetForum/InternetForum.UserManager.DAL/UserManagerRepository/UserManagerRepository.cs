using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.Administration.DAL.UserManagerRepository
{
    public class UserManagerRepository : IUserManager
    {
        private readonly UsersDbContext _context;
        private readonly UserManager<AuthUser> _manager;
        public UserManagerRepository(UserManager<AuthUser> manager, UsersDbContext context)
        {
            _context = context;
            _manager = manager;
        }
        public async Task AddUserAsync(AuthUser user)
        {
            await _context.Users.AddAsync(user);
           var rez = await _manager.CreateAsync(user);
            if (!rez.Succeeded)
                throw new ArgumentException("can not create this user");
        }

        public async Task<AuthUser> AssignUserToRoleAsync(string newRole, string userId)
        {
            AuthUser user = await _manager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("did not find user with this id");
            IdentityRole role = await _context.Roles.FindAsync(newRole);
            if (role == null)
                throw new ArgumentException("this role does not exist");

            if((await _manager.GetRolesAsync(user)).Contains(newRole))
                throw new ArgumentException("this user already have this role");

            await _manager.AddToRoleAsync(user, role.Name);

            return user;
        }

        public async Task<bool> CheckUserAuthByNameAsync(string hashPass, string username)
        {
            AuthUser user = await _manager.FindByNameAsync(username);
            if (user == null)
                throw new ArgumentException("did not find user with this username");

            if (user.PasswordHash.Equals(hashPass))
                return true;
            return false;
        }

        public async Task<bool> CheckUserAuthByEmailAsync(string hashPass, string email)
        {
            AuthUser user = await _manager.FindByEmailAsync(email);
            if (user == null)
                throw new ArgumentException("did not find user with this username");

            if (user.PasswordHash.Equals(hashPass))
                return true;
            return false;
        }

        public async Task DeleteAsync(AuthUser user)
        {
            var rez = await _manager.DeleteAsync(user);
            if (!rez.Succeeded)
                throw new ArgumentException("did not deleted user");
        }
    }
}
