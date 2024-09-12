using FluentValidation;
using JuniorHub.Application.DTOs.Application;

namespace JuniorHub.Application.Validators.Application;

public class ApplyOfferValidator : AbstractValidator<ApplyOfferDto>
{
    public ApplyOfferValidator()
    {
        RuleFor(x => x.OfferId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}