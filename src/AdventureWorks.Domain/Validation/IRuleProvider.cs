using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Validation;

public interface IRuleProvider
{
    IValidationRule DiscreetValueRule<T>(params T[] values);
    IValidationRule MaxLengthRule(int maxLength);
    IValidationRule MinLengthRule(int minLength);
    IValidationRule NotNullOrEmptyRule();
    IValidationRule RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true);
    IValidationRule RequiredRule();
    IValidationRule UniqueOnRule<T>(Expression<Func<T, object?>> keys);
}