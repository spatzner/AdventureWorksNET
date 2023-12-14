using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.SqlRepository.Validators
{
    public class AddressValidator : Domain.Person.Validation.AddressValidator
    {
        public override ValidationResult Validate(Address? entity)
        {
            var result = base.Validate(entity);

            if(new MaxLengthRule(60).IsInvalid(nameof(entity.Address1), entity!.Address1, out ValidationError? result1))
                result.Errors.Add(result1);

            if (new MaxLengthRule(60).IsInvalid(nameof(entity.Address2), entity.Address2, out ValidationError? result2))
                result.Errors.Add(result2);

            if (new MaxLengthRule(30).IsInvalid(nameof(entity.City), entity.City, out ValidationError? result3))
                result.Errors.Add(result3);

            if (new MaxLengthRule(50).IsInvalid(nameof(entity.State), entity.State, out ValidationError? result4))
                result.Errors.Add(result4);

            if (new MaxLengthRule(50).IsInvalid(nameof(entity.Country), entity.Country, out ValidationError? result5))
                result.Errors.Add(result5);

            if (new MaxLengthRule(15).IsInvalid(nameof(entity.PostalCode), entity.PostalCode, out ValidationError? result6))
                result.Errors.Add(result6);

            return result;
        }
    }
}
