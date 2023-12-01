using System.Collections;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    internal class MaxLengthAttribute : PropertyValidationAttribute
    {
        internal int MaxLength { get; }

        internal MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        internal override bool IsValid(string propertyName, object? value)
        {
            switch (value)
            {
                case null:
                    return true;
                case string s:
                    return s.Length <= MaxLength;
                case IEnumerable e:
                    int count = e.Cast<object?>().Count();
                    return count <= MaxLength;
                default:
                    throw new ArgumentException(
                        $"{nameof(MaxLengthAttribute)} is not valid for type {value.GetType()}. Attribute on property {propertyName}");
            }
        }

        internal override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                Type = ValidationType.MaxLength,
                Requirements = $"Max Length: {MaxLength}"
            };
        }
    }
}