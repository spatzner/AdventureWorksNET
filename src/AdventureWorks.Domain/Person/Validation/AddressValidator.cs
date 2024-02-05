using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public class AddressValidator(IValidationBuilder validationBuilder)
    : Validator<Address>(validationBuilder)
{
    public override ValidationResult Validate(Address entity)
    {
        return ValidationBuilder
           .NotNullOrEmptyRule()
           .Validate(entity.Type, nameof(entity.Type))
           .Validate(entity.Address1, nameof(entity.Address1))
           .Validate(entity.City, nameof(entity.City))
           .Validate(entity.State, nameof(entity.State))
           .Validate(entity.Country, nameof(entity.Country))
           .Validate(entity.PostalCode, nameof(entity.PostalCode))
           .GetResult();
    }
}