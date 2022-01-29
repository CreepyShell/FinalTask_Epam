using InternetForum.BLL.ModelsDTo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IQuestionService : ICrud<QuestionDTO>
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionsWithAnswers();
        Task<IEnumerable<QuestionDTO>> GetQuestionsByQuestionnaire(string questionnaireId);
    }
}
