﻿using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.Common.Validation;

internal class RequiredRule : ValidationRule
{
    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid = value != null;

        result = isValid ? default : GetErrorMessage(propertyName, value);

        return isValid;
    }

    protected override ValidationError GetErrorMessage(string propertyName, object? value)
    {
        return new ValidationError
        {
            Field = propertyName,
            Value = value,
            ValidationType = ValidationType.Required,
            Requirements = string.Empty
        };
    }
}