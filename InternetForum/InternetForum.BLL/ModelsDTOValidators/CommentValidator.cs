using FluentValidation;
using InternetForum.BLL.ModelsDTo;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class CommentValidator : AbstractValidator<CommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(c => c.CreatedAt).Empty();
            RuleFor(c => c.PostId).NotEmpty().WithMessage("Post id can not be empty");
            RuleFor(c => c.UserId).NotEmpty().WithMessage("User id can not be empty");
            RuleFor(c => c.CommentText).NotEmpty().MinimumLength(2).WithMessage("Comment can not be less than 2 symbols")
                .MaximumLength(140).WithMessage("Comment text can not be more than 140 symbols length");
        }
    }
}
