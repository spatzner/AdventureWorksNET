using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Services;

namespace Tests.Domain
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod]
        public async Task Test()
        {
            var sut = ServiceProvider.GetService<IPersonService>();

            PersonDetail person = new PersonDetail
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

            int id = await sut.AddPerson(person);

            Assert.IsTrue(id < 0);
        }
    }
}
