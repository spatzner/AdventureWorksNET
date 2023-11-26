using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using Dapper;

namespace AdventureWorks.SqlRepository
{
    public class AddressRepository : Repository, IAddressRepository
    {
        public AddressRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        public async Task Add(int personId, Address address)
        {


            string sql = """
                         DECLARE @AddressIdTable table(AddressId int);
                         DECLARE @AddressId int;
                         
                         INSERT INTO Person.Address (AddressLine1, AddressLine2, City, StateProvinceId, PostalCode, SpatialLocation)
                         OUTPUT inserted.AddressID INTO @AddressIdTable
                         SELECT @Address1, @Address2, @City, StateProvinceID, @PostalCode, geography::Point(@Latitude, @Longitude , 4326)
                         FROM Person.StateProvince 
                         WHERE Name = @State AND CountryRegionCode = @Country
                         
                         SELECT TOP 1 @AddressId = AddressId FROM @AddressIdTable;
                         
                         INSERT INTO Person.BusinessEntityAddress (BusinessEntityId, AddressId, AddressTypeId)
                         SELECT @PersonId, @AddressId, AddressTypeId 
                         FROM Person.AddressType where Name like @Type
                         """;

            var parameters = new
            {
                PersonId = personId,
                address.Address1,
                address.Address2,
                address.City,
                address.State,
                address.Country,
                address.PostalCode,
                address.GeoLocation.Latitude,
                address.GeoLocation.Longitude,
                address.Type
            };

            await Connection.ExecuteAsync(sql, parameters);
        }
    }
}
