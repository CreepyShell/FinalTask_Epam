using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Give user with userName a role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns>True if user assinged to role successfully, false if user already in given role</returns>
        /// <exception cref="CustomExceptions.RoleException">If role does not exist</exception>
        /// <exception cref="System.ArgumentException">If user with given username did not found</exception>
        Task<bool> AssignUserToRole(string userName, string role);
        /// <summary>
        /// Remove user with userName from role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns>True if user removedfrom role successfully, false if user does not have given role</returns>
        /// <exception cref="CustomExceptions.RoleException">If role does not exist</exception>
        /// <exception cref="System.ArgumentException">If user with given username did not found</exception>
        Task<bool> RemoveUserFromRole(string userName, string role);
        /// <summary>
        /// Return all user role
        /// </summary>
        /// <param name="username"></param>
        /// <returns>All user role</returns>
        /// <exception cref="System.ArgumentException">If did not find user with given username</exception>
        Task<IEnumerable<string>> GetUserRoles(string username);
        /// <summary>
        /// Check is given user in given role
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns>True if user have a given role, otherwise false</returns>
        /// <exception cref="CustomExceptions.RoleException">If role does not exist</exception>
        /// <exception cref="System.ArgumentException">If user with given username did not found</exception>
        Task<bool> IsInRole(string username, string role);
        /// <summary>
        /// Returns all usernames who have given roles
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Strings of usernames</returns>
        /// <exception cref="CustomExceptions.RoleException">If role does not exist</exception>
        Task<IEnumerable<string>> GetUsersInRole(string role);
    }
}
