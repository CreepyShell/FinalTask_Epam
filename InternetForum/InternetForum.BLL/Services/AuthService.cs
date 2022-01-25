using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService) : base(unitOfWork, mapper)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> AssignUserToRole(string userName, string role)
        {
            if (!await _unitOfWork.RoleManager.RoleExistsAsync(role))
                throw new UserAuthException("this role does not exist");
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (authUser == null)
                throw new UserAuthException("did not find user with this username or email");

            bool IsInRole = (await _unitOfWork.UserManager.GetRolesAsync(authUser)).Contains(role);
            if (IsInRole)
                throw new UserAuthException("user already in this role");

            return (await _unitOfWork.UserManager.AddToRoleAsync(authUser, role)).Succeeded;
        }

        public async Task<UserDTO> LogIn(AuthUserDTO logInUser, JwtSettings settings)
        {
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(logInUser.Username);
            if (authUser == null)
            {
                authUser = await _unitOfWork.UserManager.FindByEmailAsync(logInUser.Email);
                if (authUser == null)
                    throw new UserAuthException("did not find user with this username or email");
            }

            if (!SecurityHelper.VerifyPassword(logInUser.Password, authUser.PasswordHash, authUser.CodeWords))
            {
                throw new UserAuthException("password incorrect");
            }

            User user = await _unitOfWork.UserRepostory.GetUserByUsernameAsync(authUser.UserName);

            UserDTO userDTO = _mapper.Map<UserDTO>((authUser, user));
            userDTO.Token = await _tokenService.RefreshTokenAsync(authUser.UserName, settings);
            return userDTO;
        }

        public async Task<UserDTO> Register(AuthUserDTO register, JwtSettings jwtSettings)
        {
            if (await _unitOfWork.UserManager.FindByEmailAsync(register.Email) != null || await _unitOfWork.UserManager.FindByNameAsync(register.Username) != null)
                throw new UserAuthException("failed to register: email or username is not unique");
            string codeWord = "null";

            await _unitOfWork.UserManager.CreateAsync(new AuthUser()
            {
                Email = register.Email,
                PasswordHash = SecurityHelper.HashPassword(register.Password, Encoding.UTF8.GetBytes(codeWord)),
                UserName = register.Username,
                CodeWords = codeWord
            });

            AuthUser authUser = await _unitOfWork.UserManager.FindByEmailAsync(register.Email);
            await _unitOfWork.UserManager.AddToRoleAsync(authUser, "User");

            User userModel = new User() { RegisteredAt = DateTime.Now, UserName = register.Username, Id = authUser.Id };
            await _unitOfWork.UserRepostory.CreateAsync(userModel);

            UserDTO user = _mapper.Map<UserDTO>((authUser, userModel));
            user.Token = await _tokenService.GenerateTokenAsync(authUser.UserName, jwtSettings);

            await _unitOfWork.SaveChangesAsync();
            return user;
        }
    }
}
