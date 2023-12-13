namespace AdventureWorks.Domain.Person.DTOs
{
    public class QueryResult<T> : ValidationResult
    {
        public T? Result { get; set; }
    }
}