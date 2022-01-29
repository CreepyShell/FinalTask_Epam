using FluentValidation;
using InternetForum.BLL.ModelsDTo;


namespace InternetForum.BLL.ModelsDTOValidators
{
    public class AnswerValidator : AbstractValidator<AnswerDTO>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.QuestionId).NotEmpty();
            RuleFor(a => a.Text).MaximumLength(20).WithMessage("Length of answer text must be less than 20");
        }
    }
}
