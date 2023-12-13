using System.Collections;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class MaxLengthRule : IValidationRule
    {
        public int MaxLength { get; }

        public MaxLengthRule(int maxLength)
        {
            MaxLength = maxLength;
        }

        public bool Validate(string propertyName, object? value, out ValidationError? result)
        {
            bool isValid;

            switch (value)
            {
                case null:
                    isValid = true;
                    break;
                case string s:
                    isValid = s.Length <= MaxLength;
                    break;
                case IEnumerable e:
                    int count = e.Cast<object?>().Count();
                    isValid = count <= MaxLength;
                    break;
                default:
                    throw new ArgumentException(
                        $"{nameof(MaxLengthRule)} is not valid for type {value.GetType()}.");
            }

            result = !isValid ? GetErrorMessage(propertyName, value) : null;
            return isValid;
        }

        public ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                ValidationType = ValidationType.MaxLength,
                Requirements = $"Max Length: {MaxLength}"
            };
        }
    }
}