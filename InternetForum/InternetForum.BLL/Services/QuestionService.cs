using AutoMapper;
using FluentValidation.Results;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTOValidators;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        private readonly QuestionValidator _validator;
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new QuestionValidator();
        }

        public async Task<QuestionDTO> AddEntityAsync(QuestionDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Question can not be null");

            ValidationResult rez = await _validator.ValidateAsync(entity);
            if (!rez.IsValid)
                throw new InvalidOperationException("Question entity is invalid");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            await _unitOfWork.QuestionRepository.CreateAsync(_mapper.Map<Question>(entity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<QuestionDTO>(await _unitOfWork.QuestionRepository.GetByIdAsync(entity.Id));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.QuestionRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return rez;
        }

        public async Task<IEnumerable<QuestionDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<QuestionDTO>>(await _unitOfWork.QuestionRepository.GetAllAsync());
        }

        public async Task<QuestionDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<QuestionDTO>(await _unitOfWork.QuestionRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsByQuestionnaire(string questionnaireId)
        {
            return _mapper.Map<IEnumerable<QuestionDTO>>(await _unitOfWork.QuestionRepository.GetQuestionsByQuestionnaireAsync(questionnaireId));
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsWithAnswers()
        {
            return _mapper.Map<IEnumerable<QuestionDTO>>(await _unitOfWork.QuestionRepository.GetQuestionsWithAnswersAsync());
        }

        public async Task<QuestionDTO> UpdateAsync(QuestionDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("entity", "Question can not be null");

            QuestionDTO updatedQuestion = await GetByIdAsync(newEntity.Id);
            if (updatedQuestion == null) 
                throw new ArgumentException("did not find question with this id");

            ValidationResult rez = await _validator.ValidateAsync(newEntity);
            if (!rez.IsValid || updatedQuestion.QuestionnaireId != newEntity.QuestionnaireId)
                throw new InvalidOperationException($"Questionnaire entity is invalid:{string.Join(',', rez.Errors)} or tried to change questionnaire id");

            Question question = await _unitOfWork.QuestionRepository.UpdateAsync(_mapper.Map<Question>(newEntity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<QuestionDTO>(question);
        }
    }
}
