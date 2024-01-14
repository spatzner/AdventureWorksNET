using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class PersonValidator(IRuleProvider ruleProvider) : Validator<Entities.Person>(ruleProvider)
{
    public override ValidationResult Validate(Entities.Person entity)
    {
        ValidationResult result = new();

        IValidationRule requiredRule = RuleProvider.RequiredRule();

        if (requiredRule.IsInvalid(nameof(entity.Name), entity.Name, out ValidationError? result1))
            result.Errors.Add(result1);

        if (requiredRule.IsInvalid(nameof(entity.PersonType), entity.PersonType, out ValidationError? result2))
            result.Errors.Add(result2);

        return result;
    }
}

public abstract class Validator<T> : IValidator<T> where T : IValidatable
{
    protected IRuleProvider RuleProvider { get; }

    protected Validator(IRuleProvider ruleProvider)
    {
        RuleProvider = ruleProvider;
    }

    public abstract ValidationResult Validate(T entity);
}