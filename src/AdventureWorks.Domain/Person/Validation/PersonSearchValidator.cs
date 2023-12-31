﻿using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonSearchValidator : IValidator<PersonSearch>
{
    public ValidationResult Validate(PersonSearch entity)
    {
        ValidationResult result = new();

        if (new NotNullOrEmptyRule().IsInvalid(nameof(PersonSearch), entity, out ValidationError? result1))
            result.Errors.Add(result1);

        return result;
    }
}