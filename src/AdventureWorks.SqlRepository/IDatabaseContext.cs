using Dapper;

namespace AdventureWorks.SqlRepository;

internal interface IDatabaseContext : IDisposable, IAsyncDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? parameters = null);
    Task<int> ExecuteAsync(string sql, object? parameters = null);
    Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null);
}