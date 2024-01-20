using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AdventureWorks.Common.Validation;

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
                //there is no way to tell if a default value type was a validly provided value or the initialized value
                //require nullable wrappers for value types to differentiate.
                CheckValueNotValueType(value.GetType(), "Value types not supported. Use nullable type");

                isValid = value
                   .GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .Any(prop => ReferenceTypeIsNotNull(value, prop));
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

    private static void CheckValueNotValueType(Type valueType, string message)
    {
        if (valueType.IsValueType && typeof(Nullable<>).Name != valueType.Name) 
            throw new ArgumentException(message);
    }

    private static bool ReferenceTypeIsNotNull(object? value, PropertyInfo prop)
    {
        object? propValue = prop.GetValue(value);

        //there is no way to tell if a default value type was a validly provided value or the initialized value
        //require nullable wrappers for value types to differentiate.
        CheckValueNotValueType(prop.PropertyType,
            "Value types not supported for properties of class. Use nullable type for value type properties");

        return propValue != null;
    }
}