using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;
using AdventureWorks.SqlRepository;

namespace Tests.Domain
{
    [TestClass]
    public class PhoneNumberTests
    {
        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void PhoneNumber_WhenTenNumbers_CreatesFormattedNumber()
        {
            var sut = new PhoneNumber("6975550142", "Cell");

            Assert.AreEqual(sut.Number, "697-555-0142");
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void PhoneNumber_WhenThirteenNumbers_CreatesFormattedNumber()
        {
            var sut =  new PhoneNumber("1115005550174", "Cell");

            Assert.AreEqual(sut.Number, "1 (11) 500 555-0174");
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void PhoneNumber_WhenNotTenOrThirteenNumbers_Throws()
        {
            _ = new PhoneNumber("123456789", "Cell");
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MemberExpressionTest()
        {
            new UniqueRule<Address>(address => new{ address.City });
            new UniqueRule<Address>(address => address.City );

            Expression<Func<Address, object>> exp = address => new
            {
                address.Type,
                address.City
            };

            int x = 0;
        }

    }
}