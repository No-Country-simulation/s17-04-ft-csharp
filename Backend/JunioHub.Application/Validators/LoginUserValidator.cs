using FluentValidation;
using JunioHub.Application.DTOs.Identity;

namespace JunioHub.Application.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
