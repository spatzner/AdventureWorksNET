using System.Data.SqlClient;

namespace AdventureWorks.SqlRepository;

public interface IConnectionProvider
{
    SqlConnection CreateAdventureWorksConnection();
}