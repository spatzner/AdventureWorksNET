using AdventureWorks.Common.Validation;

namespace AdventureWorks.Application;

public interface IValidationService
{
    ValidationResult Validate<T>(T obj) where T : IValidatable;
}