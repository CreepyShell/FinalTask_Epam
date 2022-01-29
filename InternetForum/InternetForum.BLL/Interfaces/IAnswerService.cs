using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTo.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IAnswerService : ICrud<AnswerDTO>
    {
        Task<IEnumerable<AnswerDTO>> GetAllAnswersByQuestionId(string questionId);
        /// <summary>
        /// Return users who answer on this question
        /// </summary>
        /// <param name="answerId">Id of looking answer</param>
        /// <returns></returns>
        Task<IEnumerable<UserDTO>> GetUsersWhoAnsweredByAnswerId(string answerId);
        Task<IEnumerable<AnswerDTO>> GetMostPopularAnswersByQuestionId(string questionId);
        Task<bool> SetUserAnswer(string userId, string answerId);
        Task<bool> RemoveUserAnswer(string userId, string answerId);
        Task<bool> CheckWasUserAnswered(string userId, string answerId);
    }
}
