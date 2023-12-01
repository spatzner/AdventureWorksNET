using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    internal abstract class PropertyValidationAttribute : ValidationAttribute
    {
        internal abstract bool IsValid(string propertyName, object? value);
        internal abstract ValidationError GetErrorMessage(string propertyName, object? value);
    }
}