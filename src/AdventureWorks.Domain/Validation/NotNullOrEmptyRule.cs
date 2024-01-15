using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

internal class NotNullOrEmptyRule : ValidationRule
{
    public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
    {
        bool isValid;
        switch (value)
        {
            case null:
                isValid = false;
                break;
            case string s:
                isValid = !string.IsNullOrEmpty(s);
                break;
            case IEnumerable<object> ie:
                isValid = ie.Any();
                break;
            default:
                if (value.GetType().IsValueType)
                    throw new ArgumentException("Reference types not supported");

                isValid = value
                   .GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .Any(prop =>
                    {
                        object? val = prop.GetValue(value);
                        object? def = prop.PropertyType.IsValueType
                            ? Activator.CreateInstance(prop.PropertyType)
                            : null;

                        if (val == null && def == null)
                            return false;

                        if (val == null)
                            return true;

                        bool isMatch = val.Equals(def);
                        return !isMatch;
                    });

                break;
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
            ValidationType = ValidationType.IsNotEmpty,
            Requirements = string.Empty
        };
    }
}