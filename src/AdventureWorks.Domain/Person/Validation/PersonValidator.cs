using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonValidator(IValidationBuilder validationBuilder) : Validator<Entities.Person>(validationBuilder)
{
    public override ValidationResult Validate(Entities.Person entity)
    {
        return ValidationBuilder
           .RequiredRule()
           .Validate(entity.Name, nameof(entity.Name))
           .Validate(entity.PersonType, nameof(entity.PersonType))
           .GetResult();
    }
}