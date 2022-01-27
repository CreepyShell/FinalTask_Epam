using AutoMapper;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class QuestionnaireService : BaseService, IQuestionnaireService
    {
        public QuestionnaireService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public Task<QuestionnaireDTO> AddEntityAsync(QuestionnaireDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionnaireDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<QuestionnaireDTO> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionnaireDTO>> GetQuestionnaireWithLessQuestions(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionnaireDTO>> GetQuestionnaireWithMostQuestions(int count)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionnaireDTO> UpdateAsync(QuestionnaireDTO newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
