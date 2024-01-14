using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public interface IAddressValidator
{
    ValidationResult Validate(Address? entity);
}

public class AddressValidator(IValidationBuilder validationBuilder) : Validator<Address>(validationBuilder), IAddressValidator
{
    public override ValidationResult Validate(Address? entity)
    {
        if (entity == null)
            return ValidationBuilder.GetResult();

        ValidationBuilder.RequiredRule().Validate(nameof(entity.Type), entity.Type);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.Address1), entity.Address1);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.City), entity.City);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.State), entity.State);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.Country), entity.Country);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.PostalCode), entity.PostalCode);
        
        return ValidationBuilder.GetResult();
    }
}