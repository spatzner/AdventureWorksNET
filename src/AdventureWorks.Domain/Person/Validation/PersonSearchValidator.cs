using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonSearchValidator(IValidationBuilder validationBuilder) : Validator<PersonSearch>(validationBuilder)
{
    public override ValidationResult Validate(PersonSearch entity)
    {
        return ValidationBuilder.NotNullOrEmptyRule().Validate(entity, nameof(PersonSearch)).GetResult();
    }
}