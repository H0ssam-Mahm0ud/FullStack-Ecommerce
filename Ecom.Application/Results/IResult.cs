namespace Ecom.Application.Results;

public interface IResult
{
    string SuccessMessage { get; protected set; }
    ResultStatus Status { get; }
    IEnumerable<string> Errors { get; }
    List<ValidationError> ValidationErrors { get; }
    Type ValueType { get; }
    Object GetValue();
}
