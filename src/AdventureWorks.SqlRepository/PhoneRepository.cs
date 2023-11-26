using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using Dapper;

namespace AdventureWorks.SqlRepository
{
    public class PhoneRepository: Repository, IPhoneRepository
    {
        public PhoneRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        public async Task Add(int personId, PhoneNumber phoneNumber)
        {
            var sql = """
                      INSERT INTO Person.PersonPhone (BusinessEntityId, PhoneNumber, PhoneNumberTypeId)
                      SELECT @PersonId, @PhoneNumber, PhoneNumberTypeId
                      FROM Person.PhoneNumberType
                      WHERE Name like @Type
                      """;

            var parameters = new
            {
                PersonId = personId,
                PhoneNumber = phoneNumber.Number,
                phoneNumber.Type
            };

            await Connection.ExecuteAsync(sql, parameters);
        }
    }
}
