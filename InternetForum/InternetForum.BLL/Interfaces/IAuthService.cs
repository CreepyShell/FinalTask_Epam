using InternetForum.BLL.ModelsDTo;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Register(AuthUserDTO register);
        Task<UserDTO> LogIn(AuthUserDTO logInUser);
        //Task<bool> AssignUserToRole(UserDTO userDTO, string role);
    }
}
