using System.Data.SqlClient;

namespace AdventureWorks.SqlRepository;

internal class ConnectionProvider(ConnectionStrings connectionStrings) : IConnectionProvider
{
    public SqlConnection CreateAdventureWorksConnection()
    {
        return new SqlConnection(connectionStrings.AdventureWorks);
    }
}