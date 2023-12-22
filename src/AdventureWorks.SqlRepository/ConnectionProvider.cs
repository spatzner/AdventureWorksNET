using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain;

namespace AdventureWorks.SqlRepository
{
    internal class ConnectionProvider : IConnectionProvider
    {
        private readonly ConnectionStrings _connectionStrings;

        public ConnectionProvider(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public SqlConnection CreateAdventureWorksConnection()
        {
            return new SqlConnection(_connectionStrings.AdventureWorks);
        }
    }
}
