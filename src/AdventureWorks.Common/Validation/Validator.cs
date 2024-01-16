namespace AdventureWorks.Common.Validation;

public abstract class Validator<T>(IValidationBuilder validationBuilder) : IValidator<T> where T : IValidatable
{
    protected IValidationBuilder ValidationBuilder { get; } = validationBuilder;

    public abstract ValidationResult Validate(T entity);
}