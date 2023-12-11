using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation
{
    public class AddressValidator : IValidator<Address>
    {
        public virtual ValidationResult Validate(Address? entity)
        {
            ValidationResult result = new();

            if (entity == null)
                return result;

            var requiredRule = new RequiredRule();

            if (requiredRule.Validate(nameof(entity.Type), entity.Type, out ValidationError? result1))
                result.Errors.Add(result1!);

            if (requiredRule.Validate(nameof(entity.Address1), entity.Address1, out ValidationError? result2))
                result.Errors.Add(result2!);

            if (requiredRule.Validate(nameof(entity.City), entity.City, out ValidationError? result3))
                result.Errors.Add(result3!);

            if (requiredRule.Validate(nameof(entity.State), entity.State, out ValidationError? result4))
                result.Errors.Add(result4!);

            if (requiredRule.Validate(nameof(entity.Country), entity.Country, out ValidationError? result5))
                result.Errors.Add(result5!);

            if (requiredRule.Validate(nameof(entity.PostalCode), entity.PostalCode, out ValidationError? result6))
                result.Errors.Add(result6!);

            return result;
        }
    }
}