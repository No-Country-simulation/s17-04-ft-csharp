using FluentValidation;
using JuniorHub.Application.DTOs.Technology;

namespace JuniorHub.Application.Validators.Technology;

public class AddTechnologyValidator : AbstractValidator<TechnologyAddDto>
{
    public AddTechnologyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}