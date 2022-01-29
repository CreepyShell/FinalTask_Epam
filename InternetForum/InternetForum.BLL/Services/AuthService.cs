using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.ModelsDTOValidators;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private AuthUserValidator validations;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService) : base(unitOfWork, mapper)
        {
            _tokenService = tokenService;
        }

        public async Task<UserDTO> LogIn(AuthUserDTO logInUser, JwtSettings settings)
        {
            if (logInUser == null)
                throw new ArgumentNullException("logInUser", "logInUser is null");

            validations = new AuthUserValidator(false);
            var rez = await validations.ValidateAsync(logInUser);
            if (!rez.IsValid)
                throw new UserAuthException($"Invalid Data:{string.Join(',', rez.Errors)}");

            AuthUser authUser;
            if (string.IsNullOrEmpty(logInUser.Email))
                authUser = await _unitOfWork.UserManager.FindByNameAsync(logInUser.Username);
            else
                authUser = await _unitOfWork.UserManager.FindByEmailAsync(logInUser.Email);
            if (authUser == null)
                throw new UserAuthException("did not find user with this username or email");

            if (!SecurityHelper.VerifyPassword(logInUser.Password, authUser.PasswordHash, authUser.CodeWords))
                throw new UserAuthException("password incorrect");

            User user = await _unitOfWork.UserRepostory.GetUserByUsernameAsync(authUser.UserName);

            await _unitOfWork.UserManager.RemoveAuthenticationTokenAsync(authUser, "Provider", "RefreshToken");
            UserDTO userDTO = _mapper.Map<UserDTO>((authUser, user));
            userDTO.Token = await _tokenService.GenerateTokenAsync(authUser.UserName, settings);
            return userDTO;
        }

        public async Task LogOut(UserDTO logInUser)
        {
            if (logInUser == null)
                throw new ArgumentNullException("logInUser", "logInUser is null");

            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(logInUser.UserName);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username");

            await _unitOfWork.UserManager.RemoveAuthenticationTokenAsync(authUser, "Provider", logInUser.Token.RefreshToken);
        }

        public async Task<UserDTO> Register(AuthUserDTO register, JwtSettings jwtSettings)
        {
            if (register == null)
                throw new ArgumentNullException("register", "register entity is null");

            validations = new AuthUserValidator(true);
            var rez = await validations.ValidateAsync(register);
            if (!rez.IsValid)
                throw new UserAuthException($"Invalid Data:{string.Join(',', rez.Errors)}");

            if (await _unitOfWork.UserManager.FindByEmailAsync(register.Email) != null || await _unitOfWork.UserManager.FindByNameAsync(register.Username) != null)
                throw new UserAuthException("failed to register: email or username is not unique");
            string codeWord = "null";           

            await ValidatePassword(register.Password);
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

            await _unitOfWork.UserRepostory.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateCodeWord(UserDTO user, string newCodeWord)
        {
            if (user == null)
                throw new ArgumentNullException("user", "user is null");

            if (string.IsNullOrEmpty(newCodeWord) || newCodeWord.Length < 5)
                throw new UserAuthException("new code word is empty or length less than 5");

            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(user.UserName);
            if (authUser == null)
                throw new ArgumentException("did not find user with this username");

            authUser.CodeWords = newCodeWord;
            return (await _unitOfWork.UserManager.UpdateAsync(authUser)).Succeeded;
        }

        private async Task ValidatePassword(string pass)
        {
            foreach (var item in _unitOfWork.UserManager.PasswordValidators)
                if (!(await item.ValidateAsync(_unitOfWork.UserManager, null, pass)).Succeeded)
                    throw new UserAuthException("Invalid password");
        }
    }
}
