﻿using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonValidator : IValidator<Entities.Person>
{
    public virtual ValidationResult Validate(Entities.Person entity)
    {
        ValidationResult result = new();

        if (new RequiredRule().IsInvalid(nameof(entity.Name), entity.Name, out ValidationError? result1))
            result.Errors.Add(result1);

        if (new RequiredRule().IsInvalid(nameof(entity.PersonType), entity.PersonType, out ValidationError? result2))
            result.Errors.Add(result2);

        return result;
    }
}