using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonDetailValidator(IRuleProvider ruleProvider) : PersonValidator(ruleProvider)
{
    public ValidationResult Validate(PersonDetail entity)
    {
        ValidationResult result = base.Validate(entity);

        IValidationRule requiredRule = RuleProvider.RequiredRule();

        if (requiredRule.IsInvalid(nameof(entity.Name), entity.Name, out ValidationError? result1))
            result.Errors.Add(result1);

        if (requiredRule.IsInvalid(nameof(entity.PersonType), entity.PersonType, out ValidationError? result2))
            result.Errors.Add(result2);

        if (RuleProvider.UniqueOnRule<Address>(addr => addr.Type)
           .IsInvalid(nameof(entity.Addresses), entity.Addresses, out ValidationError? result3))
            result.Errors.Add(result3);

        if (RuleProvider.UniqueOnRule<PhoneNumber>(phone => phone.Type)
           .IsInvalid(nameof(entity.PhoneNumbers), entity.Addresses, out ValidationError? result4))
            result.Errors.Add(result4);

        if (RuleProvider.UniqueOnRule<EmailAddress>(email => email.Address)
           .IsInvalid(nameof(entity.EmailAddresses), entity.Addresses, out ValidationError? result5))
            result.Errors.Add(result5);

        return result;
    }
}