using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class OneOfRule<T> : IValidationRule
    {
        public List<T> Values { get; }
        public bool AllowNull { get; set; }

        public OneOfRule(params T[] values)
        {
            Values = values.ToList();
        }
        public bool Validate(string propertyName, object? obj, out ValidationError? result)
        {
            if (obj == null)
            {
                result = !AllowNull ? GetErrorMessage(propertyName, obj): null;
                return AllowNull;
            }

            Type valType = obj.GetType();

            if (valType is T val)
            {
                var isValid = Values.Contains(val);
                result = isValid ? GetErrorMessage(propertyName, obj) : null;
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