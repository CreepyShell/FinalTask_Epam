using FluentValidation;
using InternetForum.BLL.ModelsDTo;

namespace InternetForum.BLL.ModelsDTOValidators
{
    class ReactionValidator : AbstractValidator<ReactionDTO>
    {
        public ReactionValidator()
        {
            RuleFor(r => r.ReactedAt).Empty();
            RuleFor(r => r.IsLike).NotNull();
            RuleFor(r => r.PostId).Empty().When(p => !string.IsNullOrEmpty(p.CommentId)).WithMessage("post id must by empty when comment id is is not");
            RuleFor(r => r.CommentId).NotEmpty().When(p => string.IsNullOrEmpty(p.PostId)).WithMessage("comment id can not by empty when post id is is");
        }
    }
}
