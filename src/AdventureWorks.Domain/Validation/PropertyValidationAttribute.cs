using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class PropertyValidationAttribute : ValidationAttribute
    {
        public abstract bool IsValid(string propertyName, object? value);
        public abstract ValidationError GetErrorMessage(string propertyName, object? value);
    }
}