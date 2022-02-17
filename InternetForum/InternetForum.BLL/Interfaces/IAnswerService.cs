using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTo.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IAnswerService : ICrud<AnswerDTO>
    {
        /// <summary>
        /// Find all answers by question id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Answers with same question id</returns>
        Task<IEnumerable<AnswerDTO>> GetAllAnswersByQuestionId(string questionId);
        /// <summary>
        /// Return users who answer on this question
        /// </summary>
        /// <param name="answerId">Id of looking answer</param>
        /// <returns></returns>
        Task<IEnumerable<UserDTO>> GetUsersWhoAnsweredByAnswerId(string answerId);
        /// <summary>
        /// Find answers that was chosen by most users in question
        /// </summary>
        /// <param name="questionId">Id of question where answers are looking</param>
        /// <returns>IEnumerable of most popular answers</returns>
        Task<IEnumerable<AnswerDTO>> GetMostPopularAnswersByQuestionId(string questionId);
        /// <summary>
        /// Set unique user answer
        /// </summary>
        /// <param name="userId">Id of user that answers</param>
        /// <param name="answerId">Id of answer than chose user</param>
        /// <returns>True if user can set answer this this id(also check if question allows multiple answers),
        /// otherwise false</returns>
        Task<bool> SetUserAnswer(string userId, string answerId);
        /// <summary>
        /// Deleting user answer
        /// </summary>
        /// <param name="userId">User id which answer removed</param>
        /// <param name="answerId">Answer id which removed</param>
        /// <returns>True if removed successfully, otherwise user answer did not find false</returns>
        Task<bool> RemoveUserAnswer(string userId, string answerId);
        /// <summary>
        /// Check if user with given id answered the answer with given answer id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="answerId"></param>
        /// <returns>True if user answered, otherwise false</returns>
        Task<bool> CheckWasUserAnswered(string userId, string answerId);
    }
}
