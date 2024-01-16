using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.Common.Validation;

public abstract class ValidationRule : IValidationRule
{
    public abstract bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result);

    public bool IsInvalid(string propertyName, object? value, [NotNullWhen(true)] out ValidationError? result)
    {
        return !IsValid(propertyName, value, out result);
    }

    protected abstract ValidationError GetErrorMessage(string propertyName, object? value);
}