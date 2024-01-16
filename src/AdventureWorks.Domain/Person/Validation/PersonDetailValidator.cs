using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonDetailValidator(IValidationBuilder validationBuilder) : PersonValidator(validationBuilder)
{
    public ValidationResult Validate(PersonDetail entity)
    {
        return ValidationBuilder
           .RequiredRule()
           .Validate(entity.Name, nameof(entity.Name))
           .Validate(entity.PersonType, nameof(entity.PersonType))
           .UniqueOnRule<Address>(addr => addr.Type)
           .Validate(entity.Addresses, nameof(entity.Addresses))
           .UniqueOnRule<PhoneNumber>(phone => phone.Type)
           .Validate(entity.Addresses, nameof(entity.PhoneNumbers))
           .UniqueOnRule<EmailAddress>(email => email.Address)
           .Validate(entity.Addresses, nameof(entity.EmailAddresses))
           .GetResult();
    }
}