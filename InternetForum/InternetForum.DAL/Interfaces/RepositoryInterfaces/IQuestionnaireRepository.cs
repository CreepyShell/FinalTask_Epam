using InternetForum.DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.DAL.Interfaces.RepositoryInterfaces
{
    public interface IQuestionnaireRepository : IBaseRepository<Questionnaire>
    {
        Task<IEnumerable<Questionnaire>> GetQuestionnairesWithQuestionsAsync();
        Task<IEnumerable<Questionnaire>> GetQuestionnairesByUserIdAsync(string userId);
        Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire newQuestionnaire);
    }
}
