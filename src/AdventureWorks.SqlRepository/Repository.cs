namespace AdventureWorks.SqlRepository;

internal abstract class Repository(IDatabaseContext context) : IDisposable, IAsyncDisposable
{
    protected readonly IDatabaseContext Context = context;

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}