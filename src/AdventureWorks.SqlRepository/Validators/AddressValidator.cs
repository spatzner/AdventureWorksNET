using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.SqlRepository.Validators;

public class AddressValidator(IValidationBuilder validationBuilder)
    : Domain.Person.Validation.AddressValidator(validationBuilder)
{
    public override ValidationResult Validate(Address? entity)
    {
        if (entity == null)
            return ValidationBuilder.GetResult();

        base.Validate(entity);

        ValidationBuilder.MaxLengthRule(60).Validate(nameof(entity.Address1), entity.Address1);
        ValidationBuilder.MaxLengthRule(60).Validate(nameof(entity.Address2), entity.Address2);
        ValidationBuilder.MaxLengthRule(30).Validate(nameof(entity.City), entity.City);
        ValidationBuilder.MaxLengthRule(50).Validate(nameof(entity.State), entity.State);
        ValidationBuilder.MaxLengthRule(50).Validate(nameof(entity.Country), entity.Country);
        ValidationBuilder.MaxLengthRule(15).Validate(nameof(entity.PostalCode), entity.PostalCode);

        return ValidationBuilder.GetResult();
    }
}