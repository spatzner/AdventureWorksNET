using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.SqlRepository.Validators;

    public class AddressValidator(IValidationBuilder validationBuilder)
        : Domain.Person.Validation.AddressValidator(validationBuilder)
    {
        public override ValidationResult Validate(Address? entity)
        {
            if (entity == null)
                return ValidationBuilder.GetResult();

            base.Validate(entity);

            return ValidationBuilder
               .MaxLengthRule(60)
               .Validate(entity.Address1, nameof(entity.Address1))
               .Validate(entity.Address2, nameof(entity.Address2))
               .MaxLengthRule(30)
               .Validate(entity.City, nameof(entity.City))
               .MaxLengthRule(50)
               .Validate(entity.State, nameof(entity.State))
               .Validate(entity.Country, nameof(entity.Country))
               .MaxLengthRule(15)
               .Validate(entity.PostalCode, nameof(entity.PostalCode))
               .GetResult();
        }
    }