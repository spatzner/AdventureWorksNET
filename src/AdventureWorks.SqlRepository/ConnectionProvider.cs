using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain;

namespace AdventureWorks.SqlRepository
{
    internal class ConnectionProvider(ConnectionStrings connectionStrings) : IConnectionProvider
    {
        public SqlConnection CreateAdventureWorksConnection()
        {
            return new SqlConnection(connectionStrings.AdventureWorks);
        }
    }
}
