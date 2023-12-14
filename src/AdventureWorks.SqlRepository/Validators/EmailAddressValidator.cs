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
    public class EmailAddressValidator : Domain.Person.Validation.EmailAddressValidator
    {
        public override ValidationResult Validate(EmailAddress entity)
        {
            ValidationResult result =  base.Validate(entity);

            if(new MaxLengthRule(50).IsInvalid(nameof(entity.Address), entity.Address, out ValidationError? result1))
                result.Errors.Add(result1);

            return result;
        }
    }
}
