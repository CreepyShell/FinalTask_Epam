using InternetForum.Administration.DAL.IdentityModels;
using InternetForum.BLL.Helpers;
using InternetForum.BLL.ModelsDTo.User;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<Token> GenerateTokenAsync(string userName, JwtSettings jwtSettings);
        Task<Token> RefreshTokenAsync(string userName, string oldRefreshToken, JwtSettings jwtSettings);
        Task<bool> RemoveTokenAsync(string userName);
    }
}
