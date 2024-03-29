﻿using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public class EmailAddressValidator(IValidationBuilder validationBuilder) : Validator<EmailAddress>(validationBuilder)
{
    public override ValidationResult Validate(EmailAddress entity)
    {
        return ValidationBuilder.NotNullOrEmptyRule().Validate(entity.Address, nameof(entity.Address)).GetResult();
    }
}