using FluentValidation;
using JunioHub.Application.DTOs.Application;

namespace JunioHub.Application.Validators.Application;

public class ApplyOfferValidator : AbstractValidator<ApplyOfferDto>
{
    public ApplyOfferValidator()
    {
        RuleFor(x => x.OfferId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}