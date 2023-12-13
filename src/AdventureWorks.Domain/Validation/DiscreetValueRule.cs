using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class DiscreetValueRule<T> : IValidationRule
    {
        public List<T> Values { get; }
        public bool AllowNull { get; set; }

        public DiscreetValueRule(params T[] values)
        {
            Values = values.ToList();
        }
        public bool Validate(string propertyName, object? value, out ValidationError? result)
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

            result = null;
            return false;
        }

        public ValidationError GetErrorMessage(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }
    }
}