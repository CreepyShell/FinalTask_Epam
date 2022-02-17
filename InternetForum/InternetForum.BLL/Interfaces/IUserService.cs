using InternetForum.BLL.ModelsDTo.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> UpdateAsync(UserDTO newEntity);
        Task<UserDTO> GetByIdAsync(string id);
        Task<UserDTO> GetFullUserInfoByIdAsync(string id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
        Task<UserDTO> GetUserByNameAsync(string username);
        Task<IEnumerable<UserDTO>> GetMostActiveUsers(int count);
    }
}
