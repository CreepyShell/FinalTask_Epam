using FluentValidation;
using InternetForum.BLL.ModelsDTo;

namespace InternetForum.BLL.ModelsDTOValidators
{
    class ReactionValidator : AbstractValidator<ReactionDTO>
    {
        public ReactionValidator()
        {
            RuleFor(r => r.ReactedAt).Empty();
            RuleFor(r => r.IsLike).NotEmpty();
            RuleFor(r => r.PostId).Empty().When(p => !string.IsNullOrEmpty(p.CommentId)).WithMessage("post id must by empty when comment id is is not");
            RuleFor(r => r.CommentId).Empty().When(p => !string.IsNullOrEmpty(p.PostId)).WithMessage("comment id must by empty when post id is is not");
        }
    }
}
