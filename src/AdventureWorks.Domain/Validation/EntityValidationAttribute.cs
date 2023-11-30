using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class EntityValidationAttribute : ValidationAttribute
    {
        public abstract bool IsValid(object? value);
        public abstract ValidationError GetErrorMessage();
    }
}