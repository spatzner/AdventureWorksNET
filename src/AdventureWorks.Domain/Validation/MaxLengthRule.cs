using System.Collections;
using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

public class MaxLengthRule(int maxLength) : ValidationRule
{
    public int MaxLength { get; } = maxLength;

    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid;

        switch (value)
        {
            case null:
                isValid = true;
                break;
            case string s:
                isValid = s.Length <= MaxLength;
                break;
            case IEnumerable e:
                int count = e.Cast<object?>().Count();
                isValid = count <= MaxLength;
                break;
            default:
                throw new ArgumentException($"Type {value.GetType()} is not supported.");
        }

        result = isValid ? null : GetErrorMessage(propertyName, value);
        return isValid;
    }

    protected override ValidationError GetErrorMessage(string propertyName, object? value)
    {
        return new ValidationError
        {
            Field = propertyName,
            Value = value,
            ValidationType = ValidationType.MaxLength,
            Requirements = $"Max Length: {MaxLength}"
        };
    }
}