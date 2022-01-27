using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync();
        Task<IEnumerable<Question>> GetQuestionsByQuestionnaireAsync(string questionnaireId);
        Task<Question> UpdateAsync(Question newQuestion);
    }
}
