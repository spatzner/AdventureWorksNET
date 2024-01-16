using AdventureWorks.Domain.Person.Entities;

namespace Tests.SqlRepository.Domain;

[TestClass]
public class PhoneNumberTests
{
    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void PhoneNumber_WhenTenNumbers_CreatesFormattedNumber()
    {
        PhoneNumber sut = new("6975550142", "Cell");

        Assert.AreEqual(sut.Number, "697-555-0142");
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void PhoneNumber_WhenThirteenNumbers_CreatesFormattedNumber()
    {
        PhoneNumber sut = new("1115005550174", "Cell");

        Assert.AreEqual(sut.Number, "1 (11) 500 555-0174");
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void PhoneNumber_WhenNotTenOrThirteenNumbers_Throws()
    {
        _ = new PhoneNumber("123456789", "Cell");

        Assert.Fail();
    }
}