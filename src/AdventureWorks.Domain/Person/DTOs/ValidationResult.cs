namespace AdventureWorks.Domain.Person.DTOs
{
    public class ValidationResult
    {
        public  List<ValidationError> Errors { get; } = new();
        public bool IsValidRequest => !Errors.Any();

        public ValidationResult()
        {
        }

        protected ValidationResult(ValidationResult result)
        {
            Errors = result.Errors;
        }
    }
}