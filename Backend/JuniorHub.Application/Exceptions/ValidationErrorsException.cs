using FluentValidation.Results;

namespace JuniorHub.Application.Exceptions;

public class ValidationErrorsException : Exception
{
    public List<string> ValidationErrors { get; set; }

    public ValidationErrorsException(ValidationResult validationResult)
    {
        ValidationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}
