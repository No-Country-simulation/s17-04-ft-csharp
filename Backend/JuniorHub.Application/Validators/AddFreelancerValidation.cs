﻿using FluentValidation;
using FluentValidation.Validators;
using JuniorHub.Application.DTOs.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Validators
{
    public class AddFreelancerValidation : AbstractValidator<FreelancerAddDto>
    {
        public AddFreelancerValidation()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

            RuleFor(x => x.Technologies)
                .NotEmpty().WithMessage("At least one technology is required.")
                .Must(t => t.Count > 0).WithMessage("There must be at least one technology.");

            RuleFor(x => x.Links)
                .ForEach(link =>
                {
                    link.SetValidator(new AddLinkValidation());
                }).When(x=>x.Links is not null && x.Links.Count>0);
        }
    }
}
