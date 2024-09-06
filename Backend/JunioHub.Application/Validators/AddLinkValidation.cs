using FluentValidation;
using JunioHub.Application.DTOs.Link;

namespace JunioHub.Application.Validators
{
    public class AddLinkValidation : AbstractValidator<LinkAddDto>
    {
        public AddLinkValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required.")
                .Must(IsValidUrl).WithMessage("The URL format is invalid.");
        }

        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
