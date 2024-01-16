using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.Common.Validation;

internal class MinValueRule<T> : ValidationRule
{
    private readonly T _min;
    private readonly bool _minIncluded;

    internal MinValueRule(T min, bool minIncluded = true)
    {
        if (min is null)
            throw new ArgumentNullException(nameof(min));

        if (min is not decimal and not int)
            throw new ArgumentException($" Type {typeof(T)} is not supported");
        _min = min;
        _minIncluded = minIncluded;
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
                int @int = Convert.ToInt32(_min);
                isValid = i > @int || (_minIncluded && i == @int);
                break;
            case decimal d:
                decimal @decimal = Convert.ToDecimal(_min);
                isValid = d > @decimal || (_minIncluded && d == @decimal);
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
            Requirements = $"{_min}"
        };
    }
}