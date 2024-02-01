using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public interface IAddressValidator
{
    ValidationResult Validate(Address entity);
}