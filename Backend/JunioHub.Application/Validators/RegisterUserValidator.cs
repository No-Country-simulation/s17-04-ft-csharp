using FluentValidation;
using JunioHub.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(2, 50).WithMessage("El nombre debe tener entre 2 y 50 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .Length(2, 50).WithMessage("El apellido debe tener entre 2 y 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.")
                .Matches(@"[\W]").WithMessage("La contraseña debe contener al menos un carácter especial.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("El rol especificado no es válido.");
        }
    }
}
