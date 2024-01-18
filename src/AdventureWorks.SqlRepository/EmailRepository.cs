using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.SqlRepository;

internal class EmailRepository(IDatabaseContext context) : Repository(context), IEmailRepository
{
    public async Task AddAsync(int personId, EmailAddress emailAddress)
    {
        string sql = """
            INSERT INTO Person.EmailAddress (BusinessEntityId, EmailAddress)
            VALUES (@PersonId, @EmailAddress)
            """;

        var parameters = new { PersonId = personId, EmailAddress = emailAddress.Address };

        await Context.ExecuteAsync(sql, parameters);
    }
}