namespace AdventureWorks.Domain.Person.DTOs;

public class OperationResult : ValidationResult
{
    public bool Success { get; set; }

    public OperationResult() { }
    public OperationResult(ValidationResult validationResult) : base(validationResult) { }
}

public class AddResult : OperationResult
{
    public int Id { get; set; }
}