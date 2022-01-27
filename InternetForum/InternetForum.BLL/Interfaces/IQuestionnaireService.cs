using InternetForum.BLL.ModelsDTo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IQuestionnaireService : ICrud<QuestionnaireDTO>
    {
        Task<IEnumerable<QuestionnaireDTO>> GetQuestionnaireWithMostQuestions(int count);
        Task<IEnumerable<QuestionnaireDTO>> GetQuestionnaireWithLessQuestions(int count);
    }
}
