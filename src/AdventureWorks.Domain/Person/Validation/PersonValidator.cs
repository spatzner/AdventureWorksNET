﻿using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;

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