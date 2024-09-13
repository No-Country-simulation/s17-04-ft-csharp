using FluentValidation;
using JuniorHub.Application.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Validators
{
    internal class AddOfferValidator : AbstractValidator<OfferAddDto>
    {
        public AddOfferValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("The title cannot be empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("The description cannot be empty.");

            RuleFor(x => x.Difficult)
                .IsInEnum()
                .WithMessage("The difficulty must be a valid enum value.");

            RuleFor(x => x.State)
                .IsInEnum()
                .WithMessage("The state must be a valid enum value.");

            RuleFor(x => x.Price)
                .GreaterThan(1000m)
                .WithMessage("The price must be greater than 1000.");

            RuleFor(x=>x.EstimatedTime)
                .IsInEnum()
                .WithMessage("The estimated time must be a valid enum value.");

            RuleFor(x => x.Technologies)
                .NotEmpty()
                .WithMessage("The offer must include at least one technology.");
        }
    }
}
