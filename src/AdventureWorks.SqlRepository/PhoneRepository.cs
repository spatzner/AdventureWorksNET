using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using Dapper;

namespace AdventureWorks.SqlRepository;

public class PhoneRepository(IConnectionProvider connectionProvider) : Repository(connectionProvider), IPhoneRepository
{
    public async Task Add(int personId, PhoneNumber phoneNumber)
    {
        string sql = """
            INSERT INTO Person.PersonPhone (BusinessEntityId, PhoneNumber, PhoneNumberTypeId)
            SELECT @PersonId, @PhoneNumber, PhoneNumberTypeId
            FROM Person.PhoneNumberType
            WHERE Name like @ValidationType
            """;

        var parameters = new { PersonId = personId, PhoneNumber = phoneNumber.Number, phoneNumber.Type };

        await Connection.ExecuteAsync(sql, parameters);
    }
}