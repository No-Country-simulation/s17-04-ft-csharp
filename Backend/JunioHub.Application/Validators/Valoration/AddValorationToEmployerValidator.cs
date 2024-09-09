using FluentValidation;
using JunioHub.Application.DTOs.Valoration;

namespace JunioHub.Application.Validators.Valoration;

public class AddValorationToEmployerValidator : AbstractValidator<ValorationToEmployerDto>
{
    public AddValorationToEmployerValidator()
    {
        RuleFor(x => x.EmployerId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.ValorationValue)
            .IsInEnum().WithMessage("{PropertyName} must be a valid enum value.");
    }
}