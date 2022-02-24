using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Interfaces;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
   public  class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> AssignUserToRole(string userName, string role)
        {
            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                throw new RoleException("this role does not exist");

            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username or email");

            bool IsInRole = (await _unitOfWork.UserManager.GetRolesAsync(authUser)).Contains(role);
            if (IsInRole)
                return false;

            return (await _unitOfWork.UserManager.AddToRoleAsync(authUser, role)).Succeeded;
        }

        public async Task<bool> RemoveUserFromRole(string userName, string role)
        {
            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                throw new RoleException("this role does not exist");

            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username or email");

            bool IsInRole = (await _unitOfWork.UserManager.GetRolesAsync(authUser)).Contains(role);
            if (!IsInRole)
                return false;

            return (await _unitOfWork.UserManager.RemoveFromRoleAsync(authUser, role)).Succeeded;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string username)
        {
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(username);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username or email");

            return await _unitOfWork.UserManager.GetRolesAsync(authUser);
        }

        public async Task<bool> IsInRole(string username, string role)
        {
            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                throw new RoleException("this role does not exist");

            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(username);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username or email");

            return await _unitOfWork.UserManager.IsInRoleAsync(authUser, role);
        }

        public async Task<IEnumerable<string>> GetUsersInRole(string role)
        {
            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                throw new RoleException("this role does not exist");
            return (await _unitOfWork.UserManager.GetUsersInRoleAsync(role)).Select(u => u.UserName).ToArray();
        }
    }
}
