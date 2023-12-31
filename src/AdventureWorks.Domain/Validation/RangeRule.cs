using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Common;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

public class RangeRule : ValidationRule
{
    public decimal Min { get; }
    public decimal Max { get; }
    public bool MinInclusive { get; }
    public bool MaxInclusive { get; }

    public RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true)
    {
        Min = min;
        Max = max;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    public RangeRule(decimal min, decimal max, bool minInclusive = true, bool maxInclusive = true)
    {
        Min = min;
        Max = max;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        if (value == null)
        {
            result = null; //even though it doesn't meet requirement, RequiredRule is meant to catch nulls
            return true;
        }

        if (!value.IsIntegralValueType() && value is not decimal)
            throw new ArgumentException("Only integral value types and decimals are supported");

        decimal decValue = Convert.ToDecimal(value);

        switch (MinInclusive)
        {
            case true when decValue < Min:
            case false when decValue <= Min:
                result = GetErrorMessage(propertyName, value);
                return false;
        }

        switch (MaxInclusive)
        {
            case true when decValue > Max:
            case false when decValue >= Max:
                result = GetErrorMessage(propertyName, value);
                return false;
        }

        result = null;
        return true;
    }

    protected override ValidationError GetErrorMessage(string propertyName, object? value)
    {
        return new ValidationError
        {
            Field = propertyName,
            Value = value,
            ValidationType = ValidationType.Range,
            Requirements = $"{Min} {(MinInclusive ? "<=" : "<")} value {(MaxInclusive ? "<=" : "<")} {Max}"
        };
    }
}