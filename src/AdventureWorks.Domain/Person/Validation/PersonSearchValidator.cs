using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonSearchValidator(IValidationBuilder validationBuilder) : Validator<PersonSearch>(validationBuilder)
{
    public override ValidationResult Validate(PersonSearch entity)
    {
        ValidationBuilder.NotNullOrEmptyRule().Validate(nameof(PersonSearch), entity);
        
        return ValidationBuilder.GetResult();
    }
}