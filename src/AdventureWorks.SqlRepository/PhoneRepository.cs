using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.SqlRepository;

internal class PhoneRepository(IDatabaseContext context) : Repository(context), IPhoneRepository
{
    public async Task AddAsync(int personId, PhoneNumber phoneNumber)
    {
        string sql = """
            INSERT INTO Person.PersonPhone (BusinessEntityId, PhoneNumber, PhoneNumberTypeId)
            SELECT @PersonId, @PhoneNumber, PhoneNumberTypeId
            FROM Person.PhoneNumberType
            WHERE Name like @ValidationType
            """;

        var parameters = new { PersonId = personId, PhoneNumber = phoneNumber.Number, phoneNumber.Type };

        await Context.ExecuteAsync(sql, parameters);
    }
}