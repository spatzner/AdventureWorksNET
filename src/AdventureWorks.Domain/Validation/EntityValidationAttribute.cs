using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    internal abstract class EntityValidationAttribute : ValidationAttribute
    {
        internal abstract bool IsValid(object? value);
        internal abstract ValidationError GetErrorMessage();
    }
}