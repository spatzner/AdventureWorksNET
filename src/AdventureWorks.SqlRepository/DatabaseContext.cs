using System.Data.SqlClient;
using Dapper;

namespace AdventureWorks.SqlRepository;

internal class DatabaseContext(ConnectionStrings connectionStrings) : IDatabaseContext
{
    public SqlConnection Connection { get; } = new(connectionStrings.AdventureWorks);
    private SqlTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        CheckTransactionNotStarted();

        _transaction = (SqlTransaction)await Connection.BeginTransactionAsync();
    }

    public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? parameters = null)
    {
        return await Connection.QueryMultipleAsync(sql, parameters, _transaction);
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        return await Connection.ExecuteAsync(sql, parameters, _transaction);
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null)
    {
        return await Connection.ExecuteScalarAsync<T>(sql, parameters, _transaction);
    }

    public async Task CommitAsync()
    {
        CheckTransactionStarted();

        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        CheckTransactionStarted();

        await _transaction!.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        Connection.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
            await _transaction.DisposeAsync();
        await Connection.DisposeAsync();
    }

    private void CheckTransactionStarted()
    {
        if (_transaction == null)
            throw new InvalidOperationException("A transaction has not been started");
    }

    private void CheckTransactionNotStarted()
    {
        if (_transaction != null)
            throw new InvalidOperationException("A transaction is in progress");
    }
}