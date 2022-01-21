using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IUserRepostory : IBaseRepository<User>
    {
        Task<User> UpdateUserAsync(User updatedUser);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
