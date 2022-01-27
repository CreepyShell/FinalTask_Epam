using InternetForum.BLL.ModelsDTo.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IUserService : ICrud<UserDTO>
    {
        Task<bool> DeleteUserByNameAsync(string username);
        Task<UserDTO> GetUserByNameAsync(string username);
        Task<IEnumerable<UserDTO>> GetMostActiveUsers(int count);
    }
}
