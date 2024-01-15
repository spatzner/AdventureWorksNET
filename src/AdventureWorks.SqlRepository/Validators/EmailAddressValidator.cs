using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;
using ValidationResult = AdventureWorks.Domain.Person.DTOs.ValidationResult;

namespace AdventureWorks.SqlRepository.Validators
{
    public class EmailAddressValidator(IValidationBuilder validationBuilder) : Domain.Person.Validation.EmailAddressValidator(validationBuilder)
    {
        public override ValidationResult Validate(EmailAddress entity)
        {
            return ValidationBuilder.MaxLengthRule(50).Validate(entity.Address, nameof(entity.Address)).GetResult();
        }
    }
}
