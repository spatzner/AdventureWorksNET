﻿using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

public class MinValueRule<T> : ValidationRule
{
    public T Min { get; }
    public bool MinIncluded { get; init; }

    public MinValueRule(T min)
    {
        if (min is null)
            throw new ArgumentNullException(nameof(min));

        if (min is not decimal and not int)
            throw new ArgumentException($" Type {typeof(T)} is not supported");

        Min = min;
    }

    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid;

        if (value is null)
        {
            result = null; //even though it doesn't meet requirement, RequiredRule is meant to catch nulls
            return true;
        }

        if (typeof(T) != value.GetType())
            throw new ArgumentException($" {nameof(value)} Type {value.GetType()} must match generic type");

        switch (value)
        {
            case int i:
                int @int = Convert.ToInt32(Min);
                isValid = i > @int || (MinIncluded && i == @int);
                break;
            case decimal d:
                decimal @decimal = Convert.ToDecimal(Min);
                isValid = d > @decimal || (MinIncluded && d == @decimal);
                break;
            default:
                throw new ArgumentException($" Type {value.GetType()} is not supported");
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
            ValidationType = ValidationType.MinValue,
            Requirements = $"Min value: {Min}"
        };
    }
}