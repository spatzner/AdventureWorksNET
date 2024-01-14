using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation;

public class ValidationBuilder : IValidationBuilder
{
    private readonly ValidationResult _result = new();

    private IValidationRule? _currentValidator = null;

    public ValidationBuilder DiscreetValueRule<T>(params T[] values)
    {
        AssertValidatorNullState();
        _currentValidator = new DiscreetValueRule<T>(values);
        return this;
    }

    public ValidationBuilder MaxLengthRule(int maxLength)
    {
        AssertValidatorNullState();
        _currentValidator = new MaxLengthRule(maxLength);
        return this;
    }

    public ValidationBuilder MinLengthRule(int minLength)
    {
        AssertValidatorNullState();
        _currentValidator = new MinLengthRule(minLength);
        return this;
    }

    public ValidationBuilder NotNullOrEmptyRule()
    {
        AssertValidatorNullState();
        _currentValidator = new NotNullOrEmptyRule();
        return this;
    }

    public ValidationBuilder RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true)
    {
        AssertValidatorNullState();
        _currentValidator = new RangeRule(min, max, minInclusive, maxInclusive);
        return this;
    }

    public ValidationBuilder RequiredRule()
    {
        AssertValidatorNullState();
        _currentValidator = new RequiredRule();
        return this;
    }

    public ValidationBuilder UniqueOnRule<T>(Expression<Func<T, object?>> keys)
    {
        AssertValidatorNullState();
        _currentValidator = new UniqueOnRule<T>(keys);
        return this;
    }

    public void Validate(string propertyName, object? value)
    {
        if (_currentValidator == null)
            throw new InvalidOperationException($"Validator needs to be set before calling {nameof(Validate)}");

        if (_currentValidator.IsInvalid(propertyName, value, out ValidationError? error))
            _result.Errors.Add(error);

        _currentValidator = null;
    }

    public ValidationResult GetResult()
    {
        return _result;
    }

    private void AssertValidatorNullState()
    {
        if (_currentValidator != null)
            throw new InvalidOperationException($"current validator is already set. Call {nameof(Validate)} to finish current validation");

    }

}