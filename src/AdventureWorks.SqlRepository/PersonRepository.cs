using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
    public class PersonRepository : Repository, IPersonRepository
    {

        public PersonRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<PersonDetail> GetPerson(int id)
        {
            await using var queries = await Connection.QueryMultipleAsync(
                """
                  SELECT BusinessEntityId, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix,
                                           EmailPromotion, AdditionalContactInfo, Demographics, ModifiedDate
                                    FROM Person.Person
                                    WHERE BusinessEntityID = @Id;
                
                                    SELECT a.AddressID, a.AddressLine1, a.AddressLine2, a.City, sp.StateProvinceCode as State,
                	                      sp.CountryRegionCode as Country, a.PostalCode, a.SpatialLocation.Lat as Lat,
                	                      a.SpatialLocation.Long as Long, at.Name as AddressType
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
                        Id = addr.Id,
                        Type = addr.AddressType ?? string.Empty,
                        Address1 = addr.AddressLine1 ?? string.Empty,
                        Address2 = addr.AddressLine2 ?? string.Empty,
                        City = addr.City ?? string.Empty,
                        State = addr.State ?? string.Empty,
                        Country = addr.Country ?? string.Empty,
                        PostalCode = addr.PostalCode ?? string.Empty,
                        GeoLocation = new GeoPoint(addr.Latitude, addr.Longitude)
                    }
                ).ToList();

            List<PhoneNumber> phoneNumbers = (await queries.ReadAsync<DTO.PhoneNumber>())
                .Select(p => new PhoneNumber(p.Number ?? string.Empty, p.Type ?? string.Empty)).ToList();

            List<EmailAddress> emailAddresses = (await queries.ReadAsync<DTO.EmailAddress>())
                .Select(addr => new EmailAddress
                {
                    Id = addr.Id,
                    Address = addr.Address ?? string.Empty,
                }).ToList();

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

        public async Task<SearchResult<Person>> SearchPersons(PersonSearch criteria, int maxResults)
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
                 SELECT  BusinessEntityID as Id, PersonType, NameStyle, Title, FirstName, MiddleName, LastName, Suffix
                 INTO #PersonResults
                 FROM Person.Person
                 WHERE {string.Join(" AND ", filters)}
                 
                 SELECT TOP {maxResults} * FROM #PersonResults
                 SELECT COUNT(*) FROM #PersonResults
                 """;

            await using var queries = await Connection.QueryMultipleAsync(sql, parameters);

            var persons = (await queries.ReadAsync<DTO.Person>())
                .Select(p => p.ToEntity())
                .ToList();

            var totalCount = await queries.ReadSingleAsync<int>();

            var result = new SearchResult<Person>
            {
                Results = persons,
                Total = totalCount
            };

            return result;
        }

        public async Task<int> AddPerson(Person person)
        {
            if (person.Id != null)
                throw new ArgumentException("Cannot insert person with existing Id");

            string sql = """
                         INSERT INTO Person.BusinessEntity
                         OUTPUT Inserted.BusinessEntityID
                         DEFAULT VALUES
                         """;

            int businessEntityId = await Connection.ExecuteScalarAsync<int>(sql);

            var parameters = new
            {
                BusinessEntityId = businessEntityId,
                person.PersonType,
                person.Name.Title,
                person.Name.FirstName,
                person.Name.MiddleName,
                person.Name.LastName,
                person.Name.Suffix,
            };

            sql = """
                  INSERT INTO Person.Person
                  (BusinessEntityID, PersonType, Title, FirstName, MiddleName, LastName, Suffix)
                  VALUES
                  (@BusinessEntityID, @PersonType, @Title, @FirstName, @MiddleName, @LastName, @Suffix)
                  """;

            await Connection.ExecuteAsync(sql, parameters);

            return businessEntityId;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            if (person.Id != null)
                throw new ArgumentException("Cannot update person without existing Id");

            var parameters = new
            {
                BusinessEntityId = person.Id,
                person.PersonType,
                person.Name.Title,
                person.Name.FirstName,
                person.Name.MiddleName,
                person.Name.LastName,
                person.Name.Suffix,
            };

            var sql = """
                    UPDATE Person.Person
                    SET PersonType = @PersonType, Title = @Title, FirstName = @FirstName, 
                    MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix
                    WHERE BusinessEntityID = @BusinessEntityID
                  """;

            return await Connection.ExecuteAsync(sql, parameters);
        }
    }
}