using System.Data.SqlClient;

namespace AdventureWorks.SqlRepository
{
    public abstract class Repository : IDisposable, IAsyncDisposable
    {
        protected readonly SqlConnection Connection;

        protected Repository(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
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