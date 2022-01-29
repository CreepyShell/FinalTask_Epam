using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetAnswersByQuestionId(string questionId);
        Task<IEnumerable<Answer>> GetAnswersWithUserAnswersAsync();
        Task<Answer> UpdateAnswerAsync(Answer answer);
    }
}
