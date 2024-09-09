using FluentValidation;
using JunioHub.Application.DTOs.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Validators
{
    public class UpdateFreelancerValidation : AbstractValidator<FreelancerUpdateDto>
    {
        public UpdateFreelancerValidation()
        {
            RuleFor(freelancer => freelancer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

            RuleFor(freelancer => freelancer.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MinimumLength(2).WithMessage("LastName must be at least 2 characters long.");

            RuleFor(freelancer => freelancer.MediaUrl)
                .Must(AddLinkValidation.IsValidUrl).When(freelancer => !string.IsNullOrEmpty(freelancer.MediaUrl))
                .WithMessage("MediaUrl must be a valid URL.");

            RuleFor(freelancer => freelancer.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

            RuleFor(x => x.Links)
                .Must(links => links.All(link => !string.IsNullOrEmpty(link.Url) && Uri.IsWellFormedUriString(link.Url, UriKind.Absolute)))
                .When(x => x.Links is not null && x.Links.Count > 0);

            RuleFor(x => x.Technologies)
                .NotEmpty().WithMessage("At least one technology is required.")
                .Must(t => t.Count > 0).WithMessage("There must be at least one technology.");
        }
    }
}
