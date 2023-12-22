namespace AdventureWorks.Domain.Person.DTOs
{
    public class QueryResult<T> : OperationResult
    {
        public QueryResult()
        {
        }

        public QueryResult(T result)
        {
            Result = result;
        }

        public T? Result { get; set; }
    }
}