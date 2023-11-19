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
    public class AddressRepository : Repository, IAddressRepository
    {
        public AddressRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task Add(int personId, Address address)
        {


            string sql = """
                         DECLARE @AddressId int;
                         
                         INSERT INTO Person.Address (AddressLine1, AddressLine2, City, StateProvinceId, PostalCode, SpatialLocation)
                         OUTPUT inserted.AddressID INTO @AddressId
                         SELECT @AddressLine1, @AddressLine2, @City, StateProvinceID, @PostalCode, geography::Point(@Latitude, @Longitude , 4326)
                         FROM Person.StateProvince 
                         WHERE Name = @State AND CountryRegionCode = @Country
                         
                         INSERT INTO Person.BusinessEntityAddress (BusinessEntityId, AddressId, AddressTypeId)
                         SELECT @PersonId, @AddressId, AddressTypeId 
                         FROM Person.AddressType where Name like @Type
                         """;

            var parameters = new
            {
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
