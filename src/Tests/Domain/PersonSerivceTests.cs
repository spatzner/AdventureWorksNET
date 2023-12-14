using System.Transactions;
using AdventureWorks.Application;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace Tests.SqlRepository.Domain
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task AddPerson_WhenExcutedWithAllData_CreatesRecords()
        {
            var sut = ServiceProvider.GetService<IPersonService>();

            PersonDetail person = new()
            {
                Name = new PersonName
                {
                    Title = "Mr.",
                    FirstName = "Jim",
                    MiddleName = "A",
                    LastName = "Jam",
                    Suffix = "Sr"
                },
                PersonType = "EM",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Address1 = "123 Main St",
                        Address2 = "Suite B",
                        City = "Anytown",
                        State = "Iowa",
                        Country = "US",
                        PostalCode = "00001",
                        Type = "Home",
                        GeoLocation = new GeoPoint(1, 1)
                    }
                },
                EmailAddresses = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Address = "JimAJam@advnetureworks.com"
                    }
                },
                PhoneNumbers = new List<PhoneNumber>()
                {
                    new("1234567890", "Home")
                }
            };

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            OperationResult result = await sut.Add(person);

            Assert.IsTrue(result.Success);
        }
    }
}
