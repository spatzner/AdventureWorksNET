using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

public interface IValidationBuilder
{
    IValidationBuilder DiscreetValueRule<T>(params T[] values);
    IValidationBuilder MaxLengthRule(int maxLength);
    IValidationBuilder MinLengthRule(int minLength);
    IValidationBuilder NotNullOrEmptyRule();
    IValidationBuilder RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true);
    IValidationBuilder RequiredRule();
    IValidationBuilder UniqueOnRule<T>(Expression<Func<T, object?>> keys);
    IValidationBuilder Validate(object? value, string propertyName);
    ValidationResult GetResult();
}