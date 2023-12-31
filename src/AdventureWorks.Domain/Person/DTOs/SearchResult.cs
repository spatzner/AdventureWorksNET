namespace AdventureWorks.Domain.Person.DTOs;

public class SearchResult<T> : OperationResult
{
    public List<T>? Results { get; set; }
    public int? Total { get; set; }

    public SearchResult() { }

    public SearchResult(ValidationResult validationResult) : base(validationResult) { }
}