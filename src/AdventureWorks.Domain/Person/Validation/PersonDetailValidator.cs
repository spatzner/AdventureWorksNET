using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation
{
    public class PersonDetailValidator : PersonValidator, IValidator<PersonDetail>
    {
        public ValidationResult Validate(PersonDetail entity)
        {
            ValidationResult result = base.Validate(entity);

            if (new UniqueRule<Address>(addr => addr.Type).IsInvalid(nameof(entity.Addresses), entity.Addresses, out ValidationError? result1))
                result.Errors.Add(result1);

            if (new UniqueRule<PhoneNumber>(phone => phone.Type).IsInvalid(nameof(entity.PhoneNumbers), entity.Addresses, out ValidationError? result2))
                result.Errors.Add(result2);

            if (new UniqueRule<EmailAddress>(email => email.Address).IsInvalid(nameof(entity.EmailAddresses), entity.Addresses, out ValidationError? result3))
                result.Errors.Add(result3);

            return result;
        }
    }
}