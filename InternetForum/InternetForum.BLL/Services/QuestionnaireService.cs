using AutoMapper;
using FluentValidation.Results;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTOValidators;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class QuestionnaireService : BaseService, IQuestionnaireService
    {
        private readonly QuestionnaireValidator _validator;
        public QuestionnaireService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new QuestionnaireValidator();
        }

        public async Task<QuestionnaireDTO> AddEntityAsync(QuestionnaireDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Questionnaire can not be null");

            ValidationResult rez = await _validator.ValidateAsync(entity);
            if (!rez.IsValid)
                throw new InvalidOperationException("Questionnaire entity is invalid");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            entity.OpenAt = DateTime.Now;
            await _unitOfWork.QuestionnaireRepository.CreateAsync(_mapper.Map<Questionnaire>(entity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<QuestionnaireDTO>(await _unitOfWork.QuestionnaireRepository.GetByIdAsync(entity.Id));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.QuestionnaireRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return rez;
        }

        public async Task<IEnumerable<QuestionnaireDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<QuestionnaireDTO>>(await _unitOfWork.QuestionnaireRepository.GetAllAsync());
        }

        public async Task<QuestionnaireDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<QuestionnaireDTO>(await _unitOfWork.QuestionnaireRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<QuestionnaireDTO>>(await _unitOfWork.QuestionnaireRepository.GetQuestionnairesByUserIdAsync(userId));
        }

        public async Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesWithLessQuestions(int count)
        {

            IEnumerable<Questionnaire> questionnaires = await _unitOfWork.QuestionnaireRepository.GetQuestionnairesWithQuestionsAsync();
            return _mapper.Map<IEnumerable<QuestionnaireDTO>>
                (questionnaires.OrderBy(p => p.Questions.Count())
                .Take(count)
                .ToList());       
        }

        public async Task<IEnumerable<QuestionnaireDTO>> GetQuestionnairesWithMostQuestions(int count)
        {
            IEnumerable<Questionnaire> questionnaires = await _unitOfWork.QuestionnaireRepository.GetQuestionnairesWithQuestionsAsync();
            return _mapper.Map<IEnumerable<QuestionnaireDTO>>
                (questionnaires.OrderByDescending(p => p.Questions.Count())
                .Take(count)
                .ToList());
        }

        public async Task<QuestionnaireDTO> UpdateAsync(QuestionnaireDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("entity", "post can not be null");

            ValidationResult rez = await _validator.ValidateAsync(newEntity);
            if (!rez.IsValid)
                throw new InvalidOperationException("Post entity is invalid");

            Questionnaire questionnaire = await _unitOfWork.QuestionnaireRepository.UpdateQuestionnaireAsync(_mapper.Map<Questionnaire>(newEntity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<QuestionnaireDTO>(questionnaire);
        }
    }
}
