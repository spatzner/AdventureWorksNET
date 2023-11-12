using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using Dapper;
using Address = AdventureWorks.Domain.Person.Entities.Address;
using Person = AdventureWorks.Domain.Person.Entities.Person;
using PhoneNumber = AdventureWorks.Domain.Person.Entities.PhoneNumber;

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


    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<PersonDetail> Get(int id)
        {
            await using var queries = await Connection.QueryMultipleAsync(
                """
                  SELECT BusinessEntityId, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix,
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
                                    WHERE BusinessEntityID = @Id
                """,
                new { Id = id });

            DTO.Person sqlPerson = await queries.ReadFirstOrDefaultAsync<DTO.Person>() ??
                                   throw new ArgumentException($"Person with Id {id} does not exist");

            List<Address> addresses = (await queries.ReadAsync<DTO.Address>())
                .Select(addr =>
                    new Address
                    {
                        Address1 = addr.AddressLine1 ?? string.Empty,
                        Address2 = addr.AddressLine2 ?? string.Empty,
                        City = addr.City ?? string.Empty,
                        State = addr.State ?? string.Empty,
                        Country = addr.Country ?? string.Empty,
                        ZipCode = addr.PostalCode ?? string.Empty,
                        GeoLocation = new GeoPoint(addr.Latitude, addr.Longitude)
                    }
                ).ToList();

            List<PhoneNumber> phoneNumbers = (await queries.ReadAsync<DTO.PhoneNumber>())
                .Select(p => new PhoneNumber(p.Number ?? string.Empty, p.Type ?? string.Empty)).ToList();

            List<string> emailAddresses = (await queries.ReadAsync<string>()).ToList();

            PersonName name = new()
            {
                Title = sqlPerson.Title,
                FirstName = sqlPerson.FirstName ?? string.Empty,
                MiddleName = sqlPerson.MiddleName,
                LastName = sqlPerson.LastName ?? string.Empty,
                Suffix = sqlPerson.Suffix
            };

            PersonDetail personDetail = new()
            {
                Id = sqlPerson.BusinessEntityId,
                Name = name,
                PersonType = sqlPerson.PersonType ?? string.Empty,
                EmailAddresses = emailAddresses,
                PhoneNumbers = phoneNumbers,
                Addresses = addresses
            };

            return personDetail;
        }

        public async Task<List<Person>> Search(PersonSearch criteria)
        {
            _ = PhoneNumber.TryParse(criteria.PhoneNumber, out string phoneNumber);

            var parameters = new
            {
                EmailAddress = criteria.EmailAddress?.Trim(),
                PhoneNumber = phoneNumber,
                FirstName = criteria.FirstName?.Trim(),
                LastName = criteria.LastName?.Trim(),
                MiddleName = criteria.MiddleName?.Trim(),
                PersonType = criteria.PersonType?.Trim()
            };

            List<string> filters = new();

            if (!string.IsNullOrWhiteSpace(criteria.FirstName))
                filters.Add("FirstName like @FirstName || '%' ");
            if (!string.IsNullOrWhiteSpace(criteria.LastName))
                filters.Add("LastName like @LastName || '%' ");
            if (!string.IsNullOrWhiteSpace(criteria.MiddleName))
                filters.Add("MiddleName like @MiddleName || '%' ");
            if (!string.IsNullOrWhiteSpace(criteria.PersonType))
                filters.Add("PersonType = @PersonType");
            if (!string.IsNullOrWhiteSpace(criteria.EmailAddress))
                filters.Add(
                    "BusinessEntityID IN (SELECT BusinessEntityID FROM Person.EmailAddress WHERE EmailAddress = @EmailAddress)");
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                filters.Add(
                    "BusinessEntityID IN (SELECT BusinessEntityID FROM Person.PersonPhone WHERE PhoneNumber = @PhoneNumber)");

            if (!filters.Any())
                throw new ArgumentException("at least one property must be specified");

            string sql =
                $"""
                 SELECT BusinessEntityID as Id, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix
                 FROM Person.Person
                 WHERE {string.Join(" AND ", filters)}
                 """;

            return (await Connection.QueryAsync<DTO.Person>(sql, parameters))
                .Select(p => p.ToEntity())
                .ToList();
        }

        public async Task AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePerson(int id)
        {
            throw new NotImplementedException();
        }
    }
}