using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonDetailValidator(IValidationBuilder validationBuilder) : PersonValidator(validationBuilder)
{
    public ValidationResult Validate(PersonDetail entity)
    {
        base.Validate(entity);
        
        return ValidationBuilder
           .UniqueOnRule<Address>(addr => addr.Type)
           .Validate(entity.Addresses, nameof(entity.Addresses))
           .UniqueOnRule<PhoneNumber>(phone => phone.Type)
           .Validate(entity.PhoneNumbers, nameof(entity.PhoneNumbers))
           .UniqueOnRule<EmailAddress>(email => email.Address)
           .Validate(entity.EmailAddresses, nameof(entity.EmailAddresses))
           .GetResult();
    }
}