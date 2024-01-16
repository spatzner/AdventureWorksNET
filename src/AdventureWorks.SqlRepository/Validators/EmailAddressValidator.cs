using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using ValidationResult = AdventureWorks.Common.Validation.ValidationResult;

namespace AdventureWorks.SqlRepository.Validators;

public class EmailAddressValidator(IValidationBuilder validationBuilder)
    : Domain.Person.Validation.EmailAddressValidator(validationBuilder)
{
    public override ValidationResult Validate(EmailAddress entity)
    {
        return ValidationBuilder.MaxLengthRule(50).Validate(entity.Address, nameof(entity.Address)).GetResult();
    }
}