using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

internal class RangeRule : ValidationRule
{
    private readonly decimal _min;
    private readonly decimal _max;
    private readonly bool _minInclusive;
    private readonly bool _maxInclusive;

    internal RangeRule(decimal min, decimal max, bool minInclusive = true, bool maxInclusive = true)
    {
        _min = min;
        _max = max;
        _minInclusive = minInclusive;
        _maxInclusive = maxInclusive;
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

        switch (_minInclusive)
        {
            case true when decValue < _min:
            case false when decValue <= _min:
                result = GetErrorMessage(propertyName, value);
                return false;
        }

        switch (_maxInclusive)
        {
            case true when decValue > _max:
            case false when decValue >= _max:
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
            Requirements = $"{_min} {(_minInclusive ? "<=" : "<")} value {(_maxInclusive ? "<=" : "<")} {_max}"
        };
    }
}