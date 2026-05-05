namespace Ecom.Application.Results;

public class ValidationError
{
    public string Identifier { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorCode { get; set; }
    public ValidationSeverity ValidationSeverity { get; }
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;

    public ValidationError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ValidationError(string identifier, string errorMessage, string errorCode, ValidationSeverity validationSeverity)
    {
        Identifier = identifier;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        ValidationSeverity = validationSeverity;
    }
}
