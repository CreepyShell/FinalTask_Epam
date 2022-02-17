using FluentValidation;
using InternetForum.BLL.ModelsDTo;
using InternetForum.DAL.DomainModels;
using System;

namespace InternetForum.BLL.ModelsDTOValidators
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(p => p.CreatedAt).Empty();
            RuleFor(p => p.UpdatedAt).Empty();
            RuleFor(p => p.PostTopic).NotNull().Must(pt => CheckPostTopic(pt));
            RuleFor(p => p.Text).MaximumLength(300).WithMessage("discusstion text can be longer then 300 symbols").MinimumLength(3).When(p => !string.IsNullOrEmpty(p.Text)).WithMessage("discussion description can be less than 3 symbols if not empty");
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.Header).NotEmpty().MinimumLength(3).WithMessage("Header can not be less than 3").MaximumLength(50).WithMessage("Header can not be more than 50");
        }
        private bool CheckPostTopic(string topic)
        {
            return Enum.TryParse(typeof(PostTopic), topic, out _);
        }
    }
}
