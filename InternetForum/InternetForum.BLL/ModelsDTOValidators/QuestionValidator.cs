using FluentValidation;
using InternetForum.BLL.ModelsDTo;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class QuestionValidator : AbstractValidator<QuestionDTO>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionnaireId).NotEmpty();
            RuleFor(q => q.Text).MinimumLength(3).WithMessage("Length of question text must be more than 3").MaximumLength(40).WithMessage("Length of question text must be less than 40");
        }
    }
}

