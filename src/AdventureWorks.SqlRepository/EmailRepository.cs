using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using Dapper;

namespace AdventureWorks.SqlRepository;

public class EmailRepository(IConnectionProvider connectionProvider) : Repository(connectionProvider), IEmailRepository
{
    public async Task Add(int personId, EmailAddress emailAddress)
    {
        string sql = """
            INSERT INTO Person.EmailAddress (BusinessEntityId, EmailAddress)
            VALUES (@PersonId, @EmailAddress)
            """;

        var parameters = new { PersonId = personId, EmailAddress = emailAddress.Address };

        await Connection.ExecuteAsync(sql, parameters);
    }
}