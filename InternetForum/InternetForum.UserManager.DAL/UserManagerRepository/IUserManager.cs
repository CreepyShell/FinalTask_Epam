using System.Threading.Tasks;

namespace InternetForum.Administration.DAL.UserManagerRepository
{
    public interface IUserManager
    {
        Task AddUserAsync(AuthUser user);
        Task<AuthUser> AssignUserToRoleAsync(string role, string userId);
        Task<bool> CheckUserAuthByNameAsync(string pass, string username);
        Task<bool> CheckUserAuthByEmailAsync(string pass, string email);
        Task DeleteAsync(AuthUser user);
    }
}
