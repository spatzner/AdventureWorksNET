using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class EmailAddressValidator(IValidationBuilder validationBuilder) : Validator<EmailAddress>(validationBuilder)
{
    public override ValidationResult Validate(EmailAddress entity)
    {
        return ValidationBuilder.RequiredRule().Validate(entity.Address, nameof(entity.Address)).GetResult();
    }
}