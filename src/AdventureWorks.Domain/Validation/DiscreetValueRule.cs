﻿using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

internal class DiscreetValueRule<T>(params T[] values) : ValidationRule
{
    private readonly HashSet<T> _values = [.. values];

    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        switch (value)
        {
            case null:
                result = null; //even though it doesn't meet requirement, RequiredRule is meant to catch nulls
                return true;
            case T val:
                bool isValid = _values.Contains(val);
                result = isValid ? null : GetErrorMessage(propertyName, value);
                return isValid;
            default:
                result = GetErrorMessage(propertyName, value);
                return false;
        }
    }

    protected override ValidationError GetErrorMessage(string propertyName, object? value)
    {
        return new ValidationError
        {
            Field = propertyName,
            Value = value,
            ValidationType = ValidationType.DiscreetValue,
            Requirements = $"Accepted values: {string.Join(", ", _values.Select(x => $"'{x}'"))}"
        };
    }
}