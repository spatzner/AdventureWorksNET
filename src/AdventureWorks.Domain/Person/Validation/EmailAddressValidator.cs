using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class EmailAddressValidator(IRuleProvider ruleProvider) : Validator<EmailAddress>(ruleProvider)
{
    public override ValidationResult Validate(EmailAddress entity)
    {
        ValidationResult result = new();

        IValidationRule requiredRule = RuleProvider.RequiredRule();

        if (requiredRule.IsInvalid(nameof(entity.Address), entity.Address, out ValidationError? result1))
            result.Errors.Add(result1);

        return result;
    }
}