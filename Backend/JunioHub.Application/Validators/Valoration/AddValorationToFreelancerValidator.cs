using FluentValidation;
using JunioHub.Application.DTOs.Valoration;

namespace JunioHub.Application.Validators.Valoration;

public class AddValorationToFreelancerValidator : AbstractValidator<ValorationToFreelancerDto>
{
    public AddValorationToFreelancerValidator()
    {
        RuleFor(x => x.FreelancerId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.ValorationValue)
            .IsInEnum().WithMessage("{PropertyName} must be a valid enum value.");
    }
}