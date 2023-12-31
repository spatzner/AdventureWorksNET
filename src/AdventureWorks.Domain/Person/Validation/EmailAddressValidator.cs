using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class EmailAddressValidator : IValidator<EmailAddress>
{
    public virtual ValidationResult Validate(EmailAddress entity)
    {
        ValidationResult result = new();

        if (new RequiredRule().IsInvalid(nameof(entity.Address), entity.Address, out ValidationError? result1))
            result.Errors.Add(result1);

        return result;
    }
}