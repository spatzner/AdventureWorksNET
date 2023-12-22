using AdventureWorks.Domain.Person.DTOs;
using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;

namespace AdventureWorks.Domain.Validation
{
    public class DiscreetValueRule<T> : ValidationRule
    {
        public List<T> Values { get; }
        public bool AllowNull { get; init; }

        public DiscreetValueRule(params T[] values)
        {
            Values = values.ToList();
        }
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            if (value == null)
            {
                result = AllowNull ? null : GetErrorMessage(propertyName, value);
                return AllowNull;
            }

            if (value is T val)
            {
                var isValid = Values.Contains(val);
                result = isValid ? null : GetErrorMessage(propertyName, value) ;
                return isValid;
            };

            result = GetErrorMessage(propertyName, value);
            return false;
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