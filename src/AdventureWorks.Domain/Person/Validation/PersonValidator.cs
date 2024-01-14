using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonValidator(IValidationBuilder validationBuilder) : Validator<Entities.Person>(validationBuilder)
{
    public override ValidationResult Validate(Entities.Person entity)
    {
        ValidationBuilder.RequiredRule().Validate(nameof(entity.Name), entity.Name);
        ValidationBuilder.RequiredRule().Validate(nameof(entity.PersonType), entity.PersonType);
        
        return ValidationBuilder.GetResult();
    }
}