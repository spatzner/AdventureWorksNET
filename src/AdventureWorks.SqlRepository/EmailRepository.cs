using AdventureWorks.Domain.Person.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;
using Dapper;

namespace AdventureWorks.SqlRepository
{
    public class EmailRepository : Repository, IEmailRepository
    {
        public EmailRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task Add(int personId, EmailAddress emailAddress)
        {
            string sql = """
                         INSERT INTO Person.EmailAddress (BusinessEntityId, EmailAddress)
                         (@PersonId, @EmailAddress)
                         """;

            var parameters = new
            {
                PersonId = personId,
                EmailAddress = emailAddress
            };

            await Connection.ExecuteAsync(sql, parameters);
        }
    }
}
