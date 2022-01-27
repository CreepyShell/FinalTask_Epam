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
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            validations = new UserValidator();
        }

        public async Task<UserDTO> AddEntityAsync(UserDTO entity)
        {
            var rez = await validations.ValidateAsync(entity);
            if (!rez.IsValid)
                throw new ArgumentException($"Invalid user: {string.Join(',', rez.Errors)}");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            await _unitOfWork.UserRepostory.CreateAsync(_mapper.Map<User>(entity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDTO>(await _unitOfWork.UserRepostory.GetByIdAsync(entity.Id));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.UserRepostory.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return rez;
        }

        public async Task<bool> DeleteUserByNameAsync(string username)
        {
            User user = await _unitOfWork.UserRepostory.GetUserByUsernameAsync(username);

            bool rez = await _unitOfWork.UserRepostory.DeleteAsync(user);
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

            var rez = await validations.ValidateAsync(newEntity);
            if (!rez.IsValid)
                throw new ArgumentException("Post entity is invalid");

            User user = await _unitOfWork.UserRepostory.UpdateUserAsync(_mapper.Map<User>(newEntity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
