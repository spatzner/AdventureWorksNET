using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.Common.Validation;

public interface IValidationRule
{
    bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result);

    bool IsInvalid(string propertyName, object? value, [NotNullWhen(true)] out ValidationError? result);
}