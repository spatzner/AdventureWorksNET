using AdventureWorks.Domain.Person.DTOs;
using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;

namespace AdventureWorks.Domain.Validation
{
    public class DiscreetValueRule<T> : ValidationRule
    {
        public HashSet<T> Values { get; }

        public DiscreetValueRule(params T[] values)
        {
            Values = values.ToHashSet();
        }
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            switch (value)
            {
                case null:
                    result = null;
                    return true;
                case T val:
                    var isValid = Values.Contains(val);
                    result = isValid ? null : GetErrorMessage(propertyName, value) ;
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
                Requirements = $"Accepted values: {string.Join(", ", Values.Select(x => $"'{x}'"))}"
            };
        }
    }
}