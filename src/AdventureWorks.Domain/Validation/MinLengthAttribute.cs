using System.Collections;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class MinLengthAttribute : PropertyValidationAttribute
    {
        public int MinLength { get; }

        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }


        public override bool IsValid(string propertyName, object? value)
        {
            switch (value)
            {
                case null:
                    return false;
                case string s:
                    return s.Length >= MinLength;
                case IEnumerable e:
                    int count = e.Cast<object?>().Count();
                    return count >= MinLength;
                default:
                    throw new ArgumentException(
                        $"{nameof(MinLengthAttribute)} is not valid for type {value.GetType()}. Attribute on property {propertyName}");
            }
        }

        public override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                Type = ValidationType.MinLength,
                Requirements = $"Min Length: {MinLength}"
            };
        }
    }
}