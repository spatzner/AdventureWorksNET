namespace AdventureWorks.Common.Validation;

public interface IValidator<in T> where T : IValidatable
{
    public ValidationResult Validate(T entity);
}