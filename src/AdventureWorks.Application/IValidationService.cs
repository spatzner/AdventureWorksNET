using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Application
{
    public interface IValidationService
    {
        ValidationResult Validate<T>(T obj) where T : IValidatable;
    }
}