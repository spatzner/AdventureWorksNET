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

    private IValidationRule? _currentValidator;

    public IValidationBuilder DiscreetValueRule<T>(params T[] values)
    {
        _currentValidator = new DiscreetValueRule<T>(values);
        return this;
    }

    public IValidationBuilder MaxLengthRule(int maxLength)
    {
        _currentValidator = new MaxLengthRule(maxLength);
        return this;
    }

    public IValidationBuilder MinLengthRule(int minLength)
    {
        _currentValidator = new MinLengthRule(minLength);
        return this;
    }

    public IValidationBuilder NotNullOrEmptyRule()
    {
        _currentValidator = new NotNullOrEmptyRule();
        return this;
    }

    public IValidationBuilder RangeRule(int min, int max, bool minInclusive = true, bool maxInclusive = true)
    {
        _currentValidator = new RangeRule(min, max, minInclusive, maxInclusive);
        return this;
    }

    public IValidationBuilder RequiredRule()
    {
        _currentValidator = new RequiredRule();
        return this;
    }

    public IValidationBuilder UniqueOnRule<T>(Expression<Func<T, object?>> keys)
    {
        _currentValidator = new UniqueOnRule<T>(keys);
        return this;
    }

    public IValidationBuilder Validate(object? value, string propertyName)
    {
        if (_currentValidator == null)
            throw new InvalidOperationException($"Validator needs to be set before calling {nameof(Validate)}");

        if (_currentValidator.IsInvalid(propertyName, value, out ValidationError? error))
            _result.Errors.Add(error);

        return this;
    }

    public ValidationResult GetResult()
    {
        _currentValidator = null;
        return _result;
    }
}