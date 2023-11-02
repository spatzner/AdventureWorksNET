using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Entities;
using Dapper;
using Microsoft.SqlServer.Types;

namespace AdventureWorks.SqlRepository
{

    public abstract class Repository : IDisposable, IAsyncDisposable
    {
        protected readonly SqlConnection Connection;

        protected Repository(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }


    public class PersonRepository : Repository
    {
        public PersonRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Person> Get(int id)
        {
            await using var multi = await Connection.QueryMultipleAsync(
                @"  SELECT BusinessEntityID, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix,
                           EmailPromotion, AdditionalContactInfo, Demographics, ModifiedDate
                    FROM Person.Person
                    WHERE BusinessEntityID = @Id;

                    SELECT a.AddressID, a.AddressLine1, a.AddressLine2, a.City, sp.StateProvinceCode as State, 
	                      sp.CountryRegionCode as Country, a.PostalCode, a.SpatialLocation.Lat as Lat, 
	                      a.SpatialLocation.Long as Long, at.Name
                    FROM Person.BusinessEntityAddress bea
                    JOIN Person.Address a ON bea.AddressID = a.AddressID  
                    JOIN Person.AddressType at ON bea.AddressTypeID = at.AddressTypeID
                    JOIN Person.StateProvince sp ON a.StateProvinceID = sp.StateProvinceID
                    WHERE BusinessEntityID = @Id;

                    SELECT phoneNumber as number ,pnt.Name as type
                    FROM Person.PersonPhone pp
                    JOIN Person.PhoneNumberType pnt on pp.PhoneNumberTypeID = pnt.PhoneNumberTypeID
                    WHERE BusinessEntityID = @Id

                    SELECT EmailAddress
                    FROM Person.EmailAddress
                    WHERE BusinessEntityID = @Id",
                new { Id = id });

            Entities.Person sqlPerson = await multi.ReadFirstAsync<Entities.Person>();

            List<Address> addresses = (await multi.ReadAsync<Entities.Address>())
                .Select(addr => 
                    new Address(
                        addr.AddressLine1!, 
                        addr.AddressLine2!, 
                        addr.City!, 
                        addr.State!, 
                        addr.Country!, 
                        addr.PostalCode!,
                        new GeoPoint(addr.Latitude, addr.Longitude)
                    )
                ).ToList();

            List<PhoneNumber> phoneNumbers = (await multi.ReadAsync<PhoneNumber>()).ToList();

            List<string> emailAddresses = (await multi.ReadAsync<string>()).ToList();

            Person person = new(
                new PersonName(
                    sqlPerson.Title,
                    sqlPerson.FirstName!,
                    sqlPerson.MiddleName,
                    sqlPerson.LastName!,
                    sqlPerson.Suffix
                ),
                sqlPerson.PersonType!,
                emailAddresses,
                phoneNumbers,
                addresses,
                sqlPerson.AdditionalContactInfo,
                sqlPerson.Demographics
            );

            return person;
        }

        //Search()
        //Upsert()
        //Delete()
    }
}
