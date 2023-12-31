namespace AdventureWorks.Domain.Person.DTOs;

public class QueryResult<T> : OperationResult
{
    public T? Result { get; set; }

    public QueryResult() { }

    public QueryResult(T result)
    {
        Result = result;
    }
}