using AutoMapper;
using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.CustomExceptions;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class TokenService : BaseService, ITokenService
    {
        public TokenService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Token> GenerateTokenAsync(string userName, JwtSettings jwtSettings)
        {
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (authUser == null)
                throw new ArgumentException("Did not find user with this username");
            Token token = new Token()
            {
                RefreshToken = await _unitOfWork.UserManager.GenerateUserTokenAsync(authUser, "Provider", "RefreshToken"),
                AccessToken = SecurityHelper.GenerateJwtToken(authUser, jwtSettings, await _unitOfWork.UserManager.GetRolesAsync(authUser))
            };
            await _unitOfWork.UserManager.SetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken", token.RefreshToken);
            return token;
        }

        public async Task<string> GetTokenByUsername(string username)
        {
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(username);
            if (authUser == null)
                throw new ArgumentException("Did not find user with this username");
            string token = await _unitOfWork.UserManager.GetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken");
            return token;
        }

        public async Task<Token> RefreshTokenAsync(string oldAccessToken, string oldRefreshToken, JwtSettings settings)
        {
            string userName = JwtHelper.GetUserNameFromAccessToken(oldAccessToken, settings);
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (authUser == null)
                throw new ArgumentException("Did not find user with this username");

            string refreshToken = await _unitOfWork.UserManager.GetAuthenticationTokenAsync(authUser, "Provider", "RefreshToken");
            if (!refreshToken.Equals(oldRefreshToken))
                throw new TokenException("Invalid refresh token");
            if (string.IsNullOrEmpty(refreshToken))
                throw new TokenException("something went wrong, this user do not have a token");

            bool rez = await _unitOfWork.UserManager.VerifyUserTokenAsync(authUser, "Provider", "RefreshToken", refreshToken);
            if (!rez)
                throw new TokenException("Token is expired");

            await _unitOfWork.UserManager.RemoveAuthenticationTokenAsync(authUser, "Provider", "RefreshToken");
            return await this.GenerateTokenAsync(authUser.UserName, settings);           
        }

        public async Task<bool> RemoveTokenAsync(string username)
        {
            AuthUser authUser = await _unitOfWork.UserManager.FindByNameAsync(username);
            if (authUser == null)
                throw new ArgumentException("Did not find user with this username");
           var rez = await _unitOfWork.UserManager.RemoveAuthenticationTokenAsync(authUser, "Provider", "RefreshToken");
            return rez.Succeeded;
        }
    }
}
