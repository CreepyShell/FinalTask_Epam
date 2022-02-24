using InternetForum.BLL.Helpers;
using InternetForum.BLL.ModelsDTo.User;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates new access and refresh token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="jwtSettings">Jwt setting to generate access token</param>
        /// <returns>New generated Token</returns>
        /// <exception cref="System.ArgumentException">If user with username did not found</exception>
        Task<Token> GenerateTokenAsync(string userName, JwtSettings jwtSettings);
        /// <summary>
        /// Validate access token and refresh token, if it is valid return new tokens
        /// </summary>
        /// <param name="oldAccessToken"></param>
        /// <param name="oldRefreshToken"></param>
        /// <param name="jwtSettings">setting to generate access token</param>
        /// <returns>New access token and refresh token</returns>
        /// <exception cref="System.ArgumentException">If user with given username did not found</exception>
        /// <exception cref="CustomExceptions.TokenException">If refresh token invalid(empty, expired or not equals token in database)</exception>
        /// <exception cref="Microsoft.IdentityModel.Tokens.SecurityTokenException">If access token invalid</exception>
        Task<Token> RefreshTokenAsync(string oldAccessToken, string oldRefreshToken, JwtSettings jwtSettings);
        /// <summary>
        /// Delete from database user refresh token 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>true if user successfully log out</returns>
        /// <exception cref="System.ArgumentException">If did not find user with userName</exception>
        Task<bool> RemoveTokenAsync(string userName);
        /// <summary>
        /// Returns user refresh token
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User refresh token</returns>
        /// <exception cref="System.ArgumentException">If did not find user with userName</exception>
        Task<string> GetTokenByUsername(string username);
    }
}
