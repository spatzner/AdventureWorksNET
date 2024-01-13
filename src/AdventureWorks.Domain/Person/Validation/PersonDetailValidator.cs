﻿using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonDetailValidator : PersonValidator, IValidator<PersonDetail>
{
    public ValidationResult Validate(PersonDetail entity)
    {
        ValidationResult result = base.Validate(entity);

        if (new RequiredRule().IsInvalid(nameof(entity.Name), entity.Name, out ValidationError? result1))
            result.Errors.Add(result1);

        if (new RequiredRule().IsInvalid(nameof(entity.PersonType), entity.PersonType, out ValidationError? result2))
            result.Errors.Add(result2);

        if (new UniqueOnRule<Address>(addr => addr.Type).IsInvalid(nameof(entity.Addresses),
                entity.Addresses,
                out ValidationError? result3))
            result.Errors.Add(result3);

        if (new UniqueOnRule<PhoneNumber>(phone => phone.Type).IsInvalid(nameof(entity.PhoneNumbers),
                entity.Addresses,
                out ValidationError? result4))
            result.Errors.Add(result4);

        if (new UniqueOnRule<EmailAddress>(email => email.Address).IsInvalid(nameof(entity.EmailAddresses),
                entity.Addresses,
                out ValidationError? result5))
            result.Errors.Add(result5);

        return result;
    }
}