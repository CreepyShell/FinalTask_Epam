using AutoMapper;
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
    public class ReactionService : BaseService, IReactionService
    {
        private readonly ReactionValidator _validator;
        public ReactionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _validator = new ReactionValidator();
        }

        public async Task<IEnumerable<ReactionDTO>> GetCommentReactionsByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<ReactionDTO>>((await _unitOfWork.CommentReactionRepository.GetAllAsync()).Where(cr => cr.UserId == userId).ToList());
        }

        public async Task<IEnumerable<ReactionDTO>> GetPostReactionsByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<ReactionDTO>>((await _unitOfWork.PostReactionRepository.GetAllAsync()).Where(pr => pr.UserId == userId).ToList());
        }

        public async Task<IEnumerable<ReactionDTO>> GetReactionsByCommentId(string commentId)
        {
            return _mapper.Map<IEnumerable<ReactionDTO>>((await _unitOfWork.CommentReactionRepository.GetCommentReactionsByCommentId(commentId)).ToList());
        }

        public async Task<IEnumerable<ReactionDTO>> GetReactionsByPostId(string postId)
        {
            return _mapper.Map<IEnumerable<ReactionDTO>>((await _unitOfWork.PostReactionRepository.GetPostReactionsByPostId(postId)).ToList());
        }

        public async Task<ReactionDTO> ReactToComment(ReactionDTO reaction)
        {
            await ValidateReaction(reaction);

            CommentReaction commentReaction = await _unitOfWork.CommentReactionRepository.GetByIdAsync(reaction.Id);
            if (commentReaction == null)
            {
                await _unitOfWork.PostReactionRepository.CreateAsync(_mapper.Map<PostReaction>(reaction));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<ReactionDTO>(await _unitOfWork.CommentReactionRepository.GetByIdAsync(reaction.Id));
            }
            if (commentReaction.IsLiked == reaction.IsLike)
            {
                await _unitOfWork.CommentReactionRepository.DeleteAsync(commentReaction);
                await _unitOfWork.SaveChangesAsync();
                return null;
            }
            commentReaction.IsLiked = !reaction.IsLike;
            await _unitOfWork.CommentReactionRepository.UpdateAsync(commentReaction);
            return _mapper.Map<ReactionDTO>(commentReaction);
        }

        public async Task<ReactionDTO> ReactToPost(ReactionDTO reaction)
        {
            await ValidateReaction(reaction);

            PostReaction postReaction = await _unitOfWork.PostReactionRepository.GetByIdAsync(reaction.Id);
            if (postReaction == null)
            {
                await _unitOfWork.PostReactionRepository.CreateAsync(_mapper.Map<PostReaction>(reaction));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<ReactionDTO>(await _unitOfWork.PostReactionRepository.GetByIdAsync(reaction.Id));
            }
            if (postReaction.IsLiked == reaction.IsLike)
            {
                await _unitOfWork.PostReactionRepository.DeleteAsync(postReaction);
                await _unitOfWork.SaveChangesAsync();
                return null;
            }
            postReaction.IsLiked = !reaction.IsLike;
            await _unitOfWork.PostReactionRepository.UpdateAsync(postReaction);
            return _mapper.Map<ReactionDTO>(postReaction);
        }

        private async Task ValidateReaction(ReactionDTO reaction)
        {
            if (reaction == null)
                throw new ArgumentNullException("reaction", "reaction is null");

            var rez = await _validator.ValidateAsync(reaction);
            if (!rez.IsValid)
                throw new ArgumentException($"Invalid reaction entity:{string.Join(',', rez.Errors)}");
        }
    }
}
