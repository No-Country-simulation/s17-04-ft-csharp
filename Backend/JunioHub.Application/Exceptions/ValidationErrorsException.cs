using FluentValidation.Results;

namespace JunioHub.Application.Exceptions;

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
