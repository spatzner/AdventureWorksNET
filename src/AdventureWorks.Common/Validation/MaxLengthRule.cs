using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

internal class MaxLengthRule(int maxLength) : ValidationRule
{
    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid;

        switch (value)
        {
            case null:
                isValid = true;
                break;
            case string s:
                isValid = s.Length <= maxLength;
                break;
            case IEnumerable e:
                int count = e.Cast<object?>().Count();
                isValid = count <= maxLength;
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
            Requirements = $"{maxLength}"
        };
    }
}