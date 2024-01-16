namespace AdventureWorks.Common.Validation;

public class ValidationResult
{
    public List<ValidationError> Errors { get; } = [];
    public bool IsValidRequest => Errors.Count == 0;

    public ValidationResult() { }

    protected ValidationResult(ValidationResult result)
    {
        Errors = result.Errors;
    }
}