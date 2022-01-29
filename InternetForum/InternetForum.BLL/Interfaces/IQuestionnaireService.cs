using InternetForum.BLL.ModelsDTo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IQuestionnaireService : ICrud<QuestionnaireDTO>
    {
        Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesWithMostQuestions(int count);
        Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesWithLessQuestions(int count);
        Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesByUserId(string userId);
    }
}
