using AutoMapper;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.ModelsDTOValidators;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserValidator validations;
        private readonly IRoleService _roleService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IRoleService roleService) : base(unitOfWork, mapper)
        {
            validations = new UserValidator();
            _roleService = roleService;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.UserRepostory.RemoveUserAndUserDataAsync(id);
            await _unitOfWork.UserRepostory.SaveChangesAsync();
            await _unitOfWork.UserManager.DeleteAsync(await _unitOfWork.UserManager.FindByIdAsync(id));
            return rez;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _unitOfWork.UserRepostory.GetAllAsync());
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.UserRepostory.GetByIdAsync(id));
        }

        public async Task<UserDTO> GetFullUserInfoByIdAsync(string id)
        {
            if ((await _unitOfWork.UserRepostory.GetByIdAsync(id)) == null)
                return null;
            UserDTO userDTO = _mapper.Map<UserDTO>((await _unitOfWork.UserManager.FindByIdAsync(id), await _unitOfWork.UserRepostory.GetByIdAsync(id)));
            userDTO.Roles = (await _roleService.GetUserRoles(userDTO.UserName)).ToArray();
            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetMostActiveUsers(int count)
        {
            IEnumerable<User> users = await _unitOfWork.UserRepostory.GetUserWithComments();
            return _mapper.Map<IEnumerable<UserDTO>>
                (users.OrderByDescending(u => u.Comments.Count())
                .Take(count)
                .ToList());
        }

        public async Task<UserDTO> GetUserByNameAsync(string username)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.UserRepostory.GetUserByUsernameAsync(username));
        }

        public async Task<UserDTO> UpdateAsync(UserDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("entity", "post can not be null");

            UserDTO existUser = (await GetAllAsync()).FirstOrDefault(u => u.Id == newEntity.Id);
            if (existUser == null)
                throw new ArgumentException("did not find user with this id");
            existUser.Avatar = newEntity.Avatar;
            if (newEntity.BirthDay.HasValue)
            {
                existUser.BirthDay = newEntity.BirthDay;
                existUser.Age = (int)DateTime.Now.Subtract(newEntity.BirthDay.Value).TotalDays / 365;
            }
            existUser.Bio = newEntity.Bio;
            existUser.FullName = newEntity.FullName;
            newEntity.RegisteredAt = existUser.RegisteredAt;
            existUser.RegisteredAt = default;

            var rez = await validations.ValidateAsync(existUser);
            if (!rez.IsValid)
                throw new InvalidOperationException($"User entity is invalid:{string.Join(',',rez.Errors)}");
            existUser.RegisteredAt = newEntity.RegisteredAt;

            User user = await _unitOfWork.UserRepostory.UpdateUserAsync(_mapper.Map<User>(existUser));
            await _unitOfWork.SaveChangesAsync();
            return await GetFullUserInfoByIdAsync(user.Id);
        }
    }
}
