using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

internal class MinLengthRule(int minLength) : ValidationRule
{
    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid;

        switch (value)
        {
            case null:
                isValid = true; //even though it doesn't meet min length, RequiredRule is meant to catch nulls
                break;
            case string s:
                isValid = s.Length >= minLength;
                break;
            case IEnumerable e:
                int count = e.Cast<object?>().Count();
                isValid = count >= minLength;
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
            ValidationType = ValidationType.MinLength,
            Requirements = $"{minLength}"
        };
    }
}