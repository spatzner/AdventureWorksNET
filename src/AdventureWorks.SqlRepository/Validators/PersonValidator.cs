using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.SqlRepository.Validators;

public class PersonValidator(IValidationBuilder validationBuilder)
    : Domain.Person.Validation.PersonValidator(validationBuilder)
{
    public override ValidationResult Validate(Person entity)
    {
        base.Validate(entity);

        //TODO: Add person type validation (oneofrule)
        //entity.PersonType

        throw new NotImplementedException();
    }
}