using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Validation;

internal class RuleProvider : IRuleProvider
{
    public IValidationRule DiscreetValueRule<T>(params T[] values)
    {
        return new DiscreetValueRule<T>(values);
    }

    public IValidationRule MaxLengthRule(int maxLength)
    {
        return new MaxLengthRule(maxLength);
    }

    public IValidationRule MinLengthRule(int minLength)
    {
        return new MinLengthRule(minLength);
    }

    public IValidationRule NotNullOrEmptyRule()
    {
        return new NotNullOrEmptyRule();
    }

    public IValidationRule RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true)
    {
        return new RangeRule(min, max, minInclusive, maxInclusive);
    }

    public IValidationRule RequiredRule()
    {
        return new RequiredRule();
    }

    public IValidationRule UniqueOnRule<T>(Expression<Func<T, object?>> keys)
    {
        return new UniqueOnRule<T>(keys);
    }
}