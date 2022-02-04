using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AssignUserToRole(string userName, string role);
        Task<bool> RemoveUserFromRole(string userName, string role);
        Task<IEnumerable<string>> GetUserRoles(string username);
        Task<bool> IsInRole(string username, string role);
        Task<IEnumerable<string>> GetUsersInRole(string role);
    }
}
