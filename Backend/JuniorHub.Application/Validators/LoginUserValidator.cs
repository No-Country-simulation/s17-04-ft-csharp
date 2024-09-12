using FluentValidation;
using JuniorHub.Application.DTOs.Identity;

namespace JuniorHub.Application.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
