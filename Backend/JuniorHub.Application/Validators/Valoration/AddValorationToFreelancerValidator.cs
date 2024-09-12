using FluentValidation;
using JuniorHub.Application.DTOs.Valoration;

namespace JuniorHub.Application.Validators.Valoration;

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