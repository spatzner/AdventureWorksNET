using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonDetailValidator(IValidationBuilder validationBuilder) : PersonValidator(validationBuilder)
{
    public ValidationResult Validate(PersonDetail entity)
    {
        ValidationBuilder.RequiredRule().Validate(nameof(entity.Name), entity.Name);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.PersonType), entity.PersonType);
        ValidationBuilder.UniqueOnRule<Address>(addr => addr.Type).Validate(nameof(entity.Addresses), entity.Addresses);
        ValidationBuilder.UniqueOnRule<PhoneNumber>(phone => phone.Type)
           .Validate(nameof(entity.PhoneNumbers), entity.Addresses);
        ValidationBuilder.UniqueOnRule<EmailAddress>(email => email.Address)
           .Validate(nameof(entity.EmailAddresses), entity.Addresses);

        return ValidationBuilder.GetResult();
    }
}