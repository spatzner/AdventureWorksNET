﻿using AdventureWorks.Common.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonValidator(IValidationBuilder validationBuilder) : Validator<Entities.Person>(validationBuilder)
{
    public override ValidationResult Validate(Entities.Person entity)
    {
        return ValidationBuilder
           .RequiredRule()
           .Validate(entity.Name, nameof(entity.Name))
           .NotNullOrEmptyRule()
           .Validate(entity.PersonType, nameof(entity.PersonType))
           .GetResult();
    }
}