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
    public class CommentService : BaseService, ICommentService
    {
        private readonly CommentValidator _validator;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new CommentValidator();
        }

        public async Task<CommentDTO> AddEntityAsync(CommentDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "comment can not be null");

            ValidationResult rez = await _validator.ValidateAsync(entity);
            if (!rez.IsValid || !string.IsNullOrEmpty(entity.CommentId))
                throw new InvalidOperationException("Post entity is invalid");

            return await CreateCommentAsync(entity);
        }

        private async Task<CommentDTO> CreateCommentAsync(CommentDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            entity.CreatedAt = DateTime.Now;
            await _unitOfWork.CommentRepository.CreateAsync(_mapper.Map<Comment>(entity));
            await _unitOfWork.CommentRepository.SaveChangesAsync();
            return _mapper.Map<CommentDTO>(await _unitOfWork.CommentRepository.GetByIdAsync(entity.Id));
        }

        public async Task<CommentDTO> CreateCommentToCommentAsync(CommentDTO comment, string commentId)
        {
            if (comment == null)
                throw new ArgumentNullException("entity", "comment can not be null");

            ValidationResult rez = await _validator.ValidateAsync(comment);
            if (!rez.IsValid || await CheckIsValidCommentId(commentId) || string.IsNullOrEmpty(comment.CommentId))
                throw new InvalidOperationException($"Comment entity is invalid:{string.Join(',', rez.Errors)} or commentId is empty");


            return await CreateCommentAsync(comment);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.CommentRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommentRepository.SaveChangesAsync();
            return rez;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CommentDTO>>(await _unitOfWork.CommentRepository.GetAllAsync());
        }

        public async Task<CommentDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<CommentDTO>(await _unitOfWork.CommentRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CommentDTO>> GetMostPopularComments(int count)
        {
            IEnumerable<Comment> comments = await _unitOfWork.CommentRepository.GetCommentsWithReactions();
            return _mapper.Map<IEnumerable<CommentDTO>>
                (comments.OrderByDescending(p => p.Reactions.Count())
                .Take(count)
                .ToList());
        }

        public async Task<IEnumerable<CommentDTO>> GetMostPopularCommentsByPostId(string postId, int count)
        {
            IEnumerable<Comment> comments = await _unitOfWork.CommentRepository.GetCommentsWithReactions();
            return _mapper.Map<IEnumerable<CommentDTO>>
                (comments.OrderByDescending(p => p.Reactions.Count())
                .Where(c => c.PostId == postId)
                .Take(count)
                .ToList());
        }

        public async Task<CommentDTO> UpdateAsync(CommentDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("entity", "post can not be null");

            CommentDTO existComment = (await GetAllAsync()).FirstOrDefault(c => c.Id == newEntity.Id);
            if (existComment == null)
                throw new ArgumentException("did not find entity with this id");

            if (newEntity.PostId != existComment.PostId || newEntity.UserId != existComment.UserId || newEntity.CommentId != existComment.CommentId)
                throw new InvalidOperationException("You are trying to change postid, commentid or userid, it is invalid");

            ValidationResult rez = await _validator.ValidateAsync(newEntity);
            if (!rez.IsValid)
                throw new InvalidOperationException($"Post entity is invalid:{string.Join(',', rez.Errors)}");

            newEntity.CreatedAt = existComment.CreatedAt;
            Comment comment = await _unitOfWork.CommentRepository.UpdatePostAsync(_mapper.Map<Comment>(newEntity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CommentDTO>(comment);
        }
        private async Task<bool> CheckIsValidCommentId(string commentId) => string.IsNullOrEmpty(commentId) || (await _unitOfWork.CommentRepository.GetByIdAsync(commentId)) != null;

        public async Task<IEnumerable<CommentDTO>> GetCommentsByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<CommentDTO>>((await _unitOfWork.CommentRepository.GetAllAsync()).Where(c => c.UserId == userId).ToArray());
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByPostId(string postId)
        {
            return _mapper.Map<IEnumerable<CommentDTO>>((await _unitOfWork.CommentRepository.GetAllAsync()).Where(c => c.PostId == postId).ToArray());
        }
    }
}
