using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

public interface IValidationBuilder
{

    ValidationBuilder DiscreetValueRule<T>(params T[] values);
    ValidationBuilder MaxLengthRule(int maxLength);
    ValidationBuilder MinLengthRule(int minLength);
    ValidationBuilder NotNullOrEmptyRule();
    ValidationBuilder RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true);
    ValidationBuilder RequiredRule();
    ValidationBuilder UniqueOnRule<T>(Expression<Func<T, object?>> keys);
    void Validate(string propertyName, object? value);
    ValidationResult GetResult();
}