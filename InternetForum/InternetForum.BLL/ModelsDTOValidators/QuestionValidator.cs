using FluentValidation;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class QuestionValidator : AbstractValidator<QuestionDTO>
    {
        public QuestionValidator()
        {

        }
    }
}

