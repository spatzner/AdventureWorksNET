using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Validation;

public interface IValidator<in T> where T : IValidatable
{
    ValidationResult Validate(T entity);
}