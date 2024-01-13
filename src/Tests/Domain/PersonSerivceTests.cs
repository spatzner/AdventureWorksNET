using System.Transactions;
using AdventureWorks.Application;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace Tests.SqlRepository.Domain
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task AddPerson_WhenExecutedWithAllData_CreatesRecords()
        {
            var sut = ServiceProvider.GetService<IPersonRepository>();

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
                Addresses =
                [
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
                ],
                EmailAddresses =
                [
                    new EmailAddress { Address = "JimAJam@advnetureworks.com" }
                ],
                PhoneNumbers = [new PhoneNumber("1234567890", "Home")]
            };

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            OperationResult result = await sut.AddPerson(person);

            Assert.IsTrue(result.Success);
        }
    }
}
