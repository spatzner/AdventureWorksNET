using AdventureWorks.Domain.Person.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.Domain.Validation
{
    public class DiscreetValueRule<T> : ValidationRule
    {
        public List<T> Values { get; }
        public bool AllowNull { get; set; }

        public DiscreetValueRule(params T[] values)
        {
            Values = values.ToList();
        }
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            if (value == null)
            {
                result = !AllowNull ? GetErrorMessage(propertyName, value): null;
                return AllowNull;
            }

            Type valType = value.GetType();

            if (valType is T val)
            {
                var isValid = Values.Contains(val);
                result = isValid ? GetErrorMessage(propertyName, value) : null;
                return isValid;
            };

            result = GetErrorMessage(propertyName, value);
            return false;
        }

        protected override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }
    }
}