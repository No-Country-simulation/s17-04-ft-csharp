using FluentValidation;
using JunioHub.Application.DTOs.Technology;

namespace JunioHub.Application.Validators;

public class AddTechnologyValidator : AbstractValidator<TechnologyAddDto>
{
    public AddTechnologyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}