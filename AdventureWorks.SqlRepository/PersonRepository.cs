using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Entities;
using Dapper;

namespace AdventureWorks.SqlRepository
{

    internal abstract class Repository<T> : IDisposable, IAsyncDisposable
    {
        protected readonly SqlConnection Connection;

        protected Repository(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
        }
    }


    internal class PersonRepository : Repository<Person>
    {
        public PersonRepository(string connectionString) : base(connectionString)
        {
        }

        internal async Task<Domain.Entities.Person> Get(int id)
        {
            await using var multi = await Connection.QueryMultipleAsync(
                @"SELECT BusinessEntityID, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix,
                               EmailPromotion, AdditionalContactInfo, Demographics, ModifiedDate
                       FROM Person.Person
                       WHERE BusinessEntityID = @Id;

                       SELECT a.AddressID, a.AddressLine1, a.AddressLine2, a.City, a.StateProvinceID, a.PostalCode,
                              a.SpatialLocation, at.Name
                       FROM Person.BusinessEntityAddress bea
                       JOIN Person.Address a ON bea.AddressID = a.AddressID
                       JOIN Person.AddressType at ON bea.AddressTypeID = at.AddressTypeID
                       WHERE BusinessEntityID = @Id;",
                new { Id = id });

            Entities.Person sqlPerson = await multi.ReadFirstAsync<Entities.Person>();

            List<Address> addresses = (await multi.ReadAsync<Entities.Address>())
                .Select(addr => new Address(addr.Address1, addr.Address2, addr.City, addr.State, addr.Couunty, addr.PostalCode,
                                    new GeoPoint(addr.SpatialLocation.Lat.Value, addr.SpatialLocation.Long.Value))).ToList();

            List<string> emailAddresses = new List<string>();
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

            Person person = new Person(
                new PersonName(sqlPerson.Title, 
                                sqlPerson.FirstName ?? string.Empty, 
                                sqlPerson.MiddleName, 
                                sqlPerson.LastName ?? string.Empty, 
                                sqlPerson.Suffix),
                sqlPerson.PersonType ?? string.Empty, 
                emailAddresses, 
                phoneNumbers, 
                addresses, 
                sqlPerson.AdditionalContactInfo,
                sqlPerson.Demographics
            );

            return person;
        }

        //Get(id)
        //Search()
        //Upsert()
        //Delete()


    }
}
