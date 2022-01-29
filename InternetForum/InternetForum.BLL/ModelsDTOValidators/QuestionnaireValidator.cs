using FluentValidation;
using InternetForum.BLL.ModelsDTo;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class QuestionnaireValidator : AbstractValidator<QuestionnaireDTO>
    {
        public QuestionnaireValidator()
        {
            RuleFor(q => q.OpenAt).Empty();
            RuleFor(q => q.ClosedAt).Empty();
            RuleFor(q => q.AuthorId).NotEmpty();
            RuleFor(q => q.Title).MinimumLength(3).WithMessage("Length of questionaire title must be more than 3").MaximumLength(50).WithMessage("Length of questionaire title must be less than 50");
        }
    }
}
