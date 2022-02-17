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
    public class PostService : BaseService, IPostService
    {
        private readonly PostValidator _validator;
        public PostService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new PostValidator();
        }

        public async Task<PostDTO> AddEntityAsync(PostDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "post can not be null");

            ValidationResult rez = await _validator.ValidateAsync(entity);
            if (!rez.IsValid)
                throw new InvalidOperationException($"Post entity is invalid:{string.Join(',', rez.Errors)}");

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            entity.CreatedAt = DateTime.Now;
            await _unitOfWork.PostRepository.CreateAsync(_mapper.Map<Post>(entity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PostDTO>(await _unitOfWork.PostRepository.GetByIdAsync(entity.Id));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool rez = await _unitOfWork.PostRepository.DeleteByIdAsync(id);
            await _unitOfWork.PostRepository.SaveChangesAsync();
            return rez;
        }

        public async Task<IEnumerable<PostDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PostDTO>>(await _unitOfWork.PostRepository.GetAllAsync());
        }

        public async Task<PostDTO> GetByIdAsync(string id)
        {
            return _mapper.Map<PostDTO>(await _unitOfWork.PostRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<PostDTO>> GetMostDiscussedPosts(int count)
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetPostsWithCommentsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>
                (posts.OrderByDescending(p => p.Comments.Count())
                .Take(count)
                .ToList());
        }

        public async Task<IEnumerable<PostDTO>> GetMostPopularPosts(int count)
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetPostsWithReactionsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>
                (posts.OrderByDescending(p => p.Reactions.Count())
                .Take(count)
                .ToList());
        }

        public async Task<IEnumerable<PostDTO>> GetPostsByUsername(string username)
        {
            string userId = (await _unitOfWork.UserRepostory.GetUserByUsernameAsync(username))?.Id;
            return _mapper.Map<IEnumerable<PostDTO>>(await _unitOfWork.PostRepository.GetPostsByUserIdAsync(userId));
        }

        public async Task<IEnumerable<PostDTO>> GetPostsByDate(DateTime startTime, DateTime endTime)
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostDTO>>
                (posts.Where(p => startTime.CompareTo(p.CreatedAt) < 0 && endTime.CompareTo(p.CreatedAt) > 0)
                .ToList());
        }

        public async Task<PostDTO> UpdateAsync(PostDTO newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException("entity", "post can not be null");

            PostDTO existEntity = (await GetAllAsync()).FirstOrDefault(c => c.Id == newEntity.Id);
            if (existEntity == null)
                throw new ArgumentException("did not find user with this id");

            ValidationResult rez = await _validator.ValidateAsync(newEntity);
            if (!rez.IsValid || newEntity.UserId != existEntity.UserId) 
                throw new InvalidOperationException($"Post entity is invalid:{string.Join(',', rez.Errors)} or tried to change userId");

            newEntity.UpdatedAt = DateTime.Now;
            newEntity.CreatedAt = existEntity.CreatedAt;
            Post post = await _unitOfWork.PostRepository.UpdatePostAsync(_mapper.Map<Post>(newEntity));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PostDTO>(post);
        }
    }
}
