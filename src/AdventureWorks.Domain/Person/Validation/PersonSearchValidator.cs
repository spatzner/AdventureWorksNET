using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonSearchValidator(IValidationBuilder validationBuilder) : Validator<PersonSearch>(validationBuilder)
{
    public override ValidationResult Validate(PersonSearch entity)
    {
        return ValidationBuilder.NotNullOrEmptyRule().Validate(entity, nameof(PersonSearch)).GetResult();
    }
}