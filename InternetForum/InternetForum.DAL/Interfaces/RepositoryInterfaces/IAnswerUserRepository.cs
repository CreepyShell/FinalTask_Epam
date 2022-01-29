using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IAnswerUserRepository
    {
        Task<bool> RemoveUserAnswerAsync(string userId, string aswerId);
        Task<AnswerUser> AnswerQuestionAsync(string userId, string answerId);
        Task<IEnumerable<AnswerUser>> GetAllAsync();
        Task<IEnumerable<AnswerUser>> GetByUserIdAsync(string userId);
        Task<IEnumerable<AnswerUser>> GetByAnswerIdAsync(string answerId);
        Task<int> SaveChangesAsync();
    }
}
