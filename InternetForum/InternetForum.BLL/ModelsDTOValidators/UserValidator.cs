using FluentValidation;
using InternetForum.BLL.ModelsDTo.User;
using System.Text.RegularExpressions;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.RegisteredAt).Empty().WithMessage("RegisteredAt field must by empty");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("username can not by empty").MinimumLength(3).WithMessage("username can not by less than 3 symbols").MaximumLength(30).WithMessage("username can not by more than 30 symbols");
            RuleFor(u => u.Age).Empty().When(u => !u.BirthDay.HasValue);
            RuleFor(u => u.BirthDay).Empty().When(u => !u.Age.HasValue);
            RuleFor(u => u.Bio).MaximumLength(200).WithMessage("User bio can be longer than 200 symbols");
            RuleFor(u => u.FullName).Must(u => ValidateFullName(u)).When(u => !string.IsNullOrEmpty(u.FullName)).WithMessage("Invalid full name");
        }
        private bool ValidateFullName(string name)
        {
            if (new Regex(@"^\p{L}{2,35} $").IsMatch(name))
                return true;
            if (new Regex(@"^\p{L}{2,35} \p{L}{2,35}").IsMatch(name))
                return true;
            return false;
        }
    }
}
