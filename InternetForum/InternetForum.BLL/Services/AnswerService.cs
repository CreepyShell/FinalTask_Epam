using AutoMapper;
using FluentValidation.Results;
using InternetForum.BLL.Interfaces;
using InternetForum.BLL.ModelsDTo;
using InternetForum.BLL.ModelsDTo.User;
using InternetForum.BLL.ModelsDTOValidators;
using InternetForum.DAL.DomainModels;
using InternetForum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.BLL.Services
{
    public class AnswerService : BaseService, IAnswerService
    {
        private readonly AnswerValidator _validator;
        public AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new AnswerValidator();
        }

        public async Task<AnswerDTO> AddEntityAsync(AnswerDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Answer can not be null");

            ValidationResult rez = await _validator.ValidateAsync(entity);
            if (!rez.IsValid)
                throw new ArgumentException("Answer entity is invalid");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            await _unitOfWork.AnswerRepository.CreateAsync(_mapper.Map<Answer>(entity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AnswerDTO>(await _unitOfWork.AnswerRepository.GetByIdAsync(entity.Id));
        }

        public async Task<bool> CheckWasUserAnswered(string userId, string answerId)
        {
            return (await _unitOfWork.AnswerUserRepository.GetByUserIdAsync(userId)).Select(au => au.AnswerId).Contains(answerId);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.AnswerRepository.DeleteByIdAsync(id);
            await _unitOfWork.AnswerRepository.SaveChangesAsync();
            return rez;
        }

        public async Task<IEnumerable<AnswerDTO>> GetAllAnswersByQuestionId(string questionId)
        {
            return _mapper.Map<IEnumerable<AnswerDTO>>(await _unitOfWork.AnswerRepository.GetAnswersByQuestionId(questionId));
        }

        public async Task<IEnumerable<AnswerDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AnswerDTO>>(await _unitOfWork.AnswerRepository.GetAllAsync());
        }

        public async Task<AnswerDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<AnswerDTO>(await _unitOfWork.AnswerRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<AnswerDTO>> GetMostPopularAnswersByQuestionId(string questionId)
        {
            return _mapper.Map<IEnumerable<AnswerDTO>>(
                (await _unitOfWork.AnswerRepository
                .GetAnswersWithUserAnswersAsync())
                .Where(a => a.QuestionId == questionId).OrderByDescending(a => a.Users.Count())
                );
        }

        public async Task<IEnumerable<UserDTO>> GetUsersWhoAnsweredByAnswerId(string answerId)
        {
            string[] userIds = (await _unitOfWork.AnswerUserRepository.GetByAnswerIdAsync(answerId)).Select(a => a.UserId).ToArray();
            IEnumerable<User> users = (await _unitOfWork.UserRepostory.GetAllAsync()).Where(u => userIds.Contains(u.Id));
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> RemoveUserAnswer(string userId, string answerId)
        {
            await _unitOfWork.AnswerUserRepository.RemoveUserAnswerAsync(userId, answerId);
            await _unitOfWork.AnswerUserRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetUserAnswer(string userId, string answerId)
        {
            Answer answer = await _unitOfWork.AnswerRepository.GetByIdAsync(answerId);
            Question question = await _unitOfWork.QuestionRepository.GetByIdAsync(answer.QuestionId);
            if (!question.IsAllowedMultiple)
            {
                IEnumerable<string> answersIds = (await _unitOfWork.AnswerRepository.GetAnswersByQuestionId(question.Id)).Select(a => a.Id);
                IEnumerable<AnswerUser> answerUsers = await _unitOfWork.AnswerUserRepository.GetByUserIdAsync(userId);
                foreach (var item in answerUsers)
                {
                    if (answersIds.Contains(item.AnswerId))
                        return false;
                }
            }

            await _unitOfWork.AnswerUserRepository.AnswerQuestionAsync(userId, answerId);
            await _unitOfWork.AnswerUserRepository.SaveChangesAsync();
            return true;
        }

        public async Task<AnswerDTO> UpdateAsync(AnswerDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("Answer", "post can not be null");

            Answer answer = await _unitOfWork.AnswerRepository.GetByIdAsync(newEntity.Id);
            if (answer == null || answer.Text.Length > 20) 
                throw new ArgumentException("did not find answer with this id or answer text is incorect");
            answer.Text = newEntity.Text;

            Answer updatedAnswer = await _unitOfWork.AnswerRepository.UpdateAnswerAsync(answer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AnswerDTO>(updatedAnswer);
        }
    }
}
