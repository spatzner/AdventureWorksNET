using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class EmailAddressValidator(IValidationBuilder validationBuilder) : Validator<EmailAddress>(validationBuilder)
{
    public override ValidationResult Validate(EmailAddress entity)
    {
       ValidationBuilder.RequiredRule().Validate(nameof(entity.Address), entity.Address);

        return ValidationBuilder.GetResult();
    }
}