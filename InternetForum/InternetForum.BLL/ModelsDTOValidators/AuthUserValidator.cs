using FluentValidation;
using InternetForum.BLL.ModelsDTo.User;
using System.Linq;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class AuthUserValidator : AbstractValidator<AuthUserDTO>
    {
        public AuthUserValidator(bool isRegister)
        {
            if (isRegister)
            {
                RuleFor(u => u.Password).NotEmpty().WithMessage("password can not by empty");
                RuleFor(u => u.Username).NotEmpty().WithMessage("username can not by empty").MinimumLength(3).WithMessage("username can not by less than 3 symbols").MaximumLength(30).WithMessage("username can not by more than 30 symbols")
                    .Must(u => !u.ToCharArray().Any(ar => char.IsWhiteSpace(ar)));
                RuleFor(u => u.Email).NotEmpty().WithMessage("email can not by empty").EmailAddress().WithMessage("Invalid email");
            }
            else
            {
                RuleFor(u => u.Password).NotEmpty().WithMessage("password can not be empty");
                RuleFor(u => u.Username).Empty().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage("username can not by empty when email is empty").MinimumLength(3).WithMessage("username can not by less than 3 symbols").MaximumLength(30).WithMessage("Username can not by more than 30 symbols");
                RuleFor(u => u.Email).Empty().When(u => !string.IsNullOrEmpty(u.Username)).WithMessage("email can not by empty when username is empty");
            }
        }
    }
}
