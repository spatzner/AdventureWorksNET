using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public interface IAddressValidator
{
    ValidationResult Validate(Address? entity);
}

public class AddressValidator(IValidationBuilder validationBuilder)
    : Validator<Address>(validationBuilder), IAddressValidator
{
    public override ValidationResult Validate(Address? entity)
    {
        if (entity == null)
            return ValidationBuilder.GetResult();

        return ValidationBuilder
           .RequiredRule()
           .Validate(entity.Type, nameof(entity.Type))
           .Validate(entity.Address1, nameof(entity.Address1))
           .Validate(entity.City, nameof(entity.City))
           .Validate(entity.State, nameof(entity.State))
           .Validate(entity.Country, nameof(entity.Country))
           .Validate(entity.PostalCode, nameof(entity.PostalCode))
           .GetResult();
    }
}