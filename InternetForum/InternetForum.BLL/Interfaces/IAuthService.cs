using InternetForum.BLL.Helpers;
using InternetForum.BLL.ModelsDTo.User;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Register new user, give it base role 'User' and add it to databases
        /// </summary>
        /// <param name="register">Register user data</param>
        /// <param name="settings">Setting to generate new jwt token</param>
        /// <returns>New user with generated access and refresh token</returns>
        /// <exception cref="System.ArgumentNullException">When register is null</exception>
        /// <exception cref="System.InvalidOperationException">When register model is invalid</exception>
        /// <exception cref="CustomExceptions.UserAuthException">When user with given username or email already exist or invalid password</exception>
        /// <exception cref="System.NullReferenceException">When jwt setting incorect</exception>
        Task<UserDTO> Register(AuthUserDTO register, JwtSettings settings);
        /// <summary>
        /// Log in user
        /// </summary>
        /// <param name="logInUser">Login user data</param>
        /// <param name="settings">Setting to generate jwt token</param>
        /// <returns>Log in user with generated access and refresh tokens</returns>
        ///  /// <exception cref="System.ArgumentNullException">When login user is null</exception>
        /// <exception cref="System.InvalidOperationException">When login data is invalid</exception>
        /// <exception cref="System.ArgumentException">When did not find user with given username or email</exception>
        /// <exception cref="CustomExceptions.UserAuthException">When incorect password</exception>
        /// <exception cref="System.NullReferenceException">When jwt setting incorect</exception>
        Task<UserDTO> LogIn(AuthUserDTO logInUser, JwtSettings settings);
        /// <summary>
        /// Log out user and remove refresh token from database
        /// </summary>
        /// <param name="logInUser">user who log out</param>
        /// <exception cref="System.ArgumentNullException">When argument is null</exception>
        /// <exception cref="System.ArgumentException">When did not find user with given username or token is null</exception>
        /// <exception cref="System.InvalidOperationException">When given refresh token not equals token in database</exception>
        /// <returns></returns>
        Task LogOut(UserDTO logInUser);
        /// <summary>
        /// Updates user code words in database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newCodeWord"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">When argument is null</exception>
        /// <exception cref="CustomExceptions.UserAuthException">When new code words invalid</exception>
        /// <exception cref="System.ArgumentException">When did not find user with given username</exception>
        Task<bool> UpdateCodeWord(UserDTO user, string newCodeWord);
        /// <summary>
        /// Updated user password
        /// </summary>
        /// <param name="username">Username whose password will update</param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="settings">Setting to generate jwt token</param>
        /// <returns>New access and refresh token</returns>
        /// <exception cref="System.ArgumentException">When did not find user with given username</exception>
        /// <exception cref="System.InvalidOperationException">When passwords mismatch ot new password invalid</exception>
        /// <exception cref="System.NullReferenceException">When jwt setting invalid </exception>
        Task<Token> UpdatePassword(string username, string currentPassword, string newPassword, JwtSettings settings);
    }
}
