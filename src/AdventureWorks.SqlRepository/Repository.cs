using System.Data.SqlClient;

namespace AdventureWorks.SqlRepository
{
    public abstract class Repository(IConnectionProvider connectionProvider) : IDisposable, IAsyncDisposable
    {
        protected readonly SqlConnection Connection = connectionProvider.CreateAdventureWorksConnection();

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}