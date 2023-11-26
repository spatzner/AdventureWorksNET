using System.Data.SqlClient;

namespace AdventureWorks.SqlRepository
{
    public abstract class Repository : IDisposable, IAsyncDisposable
    {
        protected readonly SqlConnection Connection;

        protected Repository(IConnectionProvider connectionProvider)
        {
            Connection = connectionProvider.CreateAdventureWorksConnection();
        }

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