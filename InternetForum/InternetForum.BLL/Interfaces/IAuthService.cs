using InternetForum.BLL.Helpers;
using InternetForum.BLL.ModelsDTo.User;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Register(AuthUserDTO register, JwtSettings settings);
        Task<UserDTO> LogIn(AuthUserDTO logInUser, JwtSettings settings);
        Task<bool> AssignUserToRole(string userName, string role);
    }
}
