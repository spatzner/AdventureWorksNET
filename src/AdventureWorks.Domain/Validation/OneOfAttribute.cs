using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    internal class OneOfAttribute<T> : PropertyValidationAttribute
    {
        internal List<T> Values { get; }
        internal bool AllowNull { get; set; }

        internal OneOfAttribute(params T[] values)
        {
            Values = values.ToList();
        }
        internal override bool IsValid(string propertyName, object? value)
        {
            if (value == null)
                return AllowNull;

            Type valType = value.GetType();

            if (valType is T val)
                return Values.Contains(val);

            return false;
        }

        internal override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }
    }
}