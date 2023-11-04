using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.SqlRepository;

namespace Tests;

[TestClass]
public class PersonRepositoryTests
{
    private static PersonRepository? _sut;
    private static PersonDetail? _testPerson;

    private static readonly string FailedRetrievalMessage = "Failed to retrieve person";

    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        _sut = new PersonRepository(Settings.ConnectionString);

        try
        {
            _testPerson = await _sut.GetPerson(291);
        }
        catch
        {
            // ignored
        }
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsId()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsTrue(_testPerson.Id > 0);
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsName()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.Prefix));
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.FirstName));
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.MiddleName));
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.LastName));
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.Suffix));
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsPersonType()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.PersonType));
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsEmailAddresses()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsTrue(_testPerson.EmailAddresses.Any());
        Assert.IsFalse(_testPerson.EmailAddresses.Any(string.IsNullOrWhiteSpace));
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsPhoneNumbers()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsTrue(_testPerson.PhoneNumbers.Any());
        Assert.IsFalse(_testPerson.PhoneNumbers.Any(x=> string.IsNullOrWhiteSpace(x.Number)));
        Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsAddresses()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsTrue(_testPerson.Addresses.Any());
        Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Number)));
        Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsAdditionalContactInfo()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsNotNull(_testPerson.AdditionalContactInfo);
    }

    [TestMethod]
    [TestCategory(Constants.Integration)]
    public void GetPerson_WhenExists_ReturnsDemographics()
    {
        Assert.IsNotNull(_testPerson, FailedRetrievalMessage);
        Assert.IsNotNull(_testPerson.Demographics);
    }
}


