using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace Tests.SqlRepository.SqlRepository;

public class PersonRepositoryTests
{
    [TestClass]
    public class GetPersonTests
    {
        private static IPersonRepository? _sut;
        private static PersonDetail? _testPerson;
        private static Exception? _exception;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            _sut = ServiceProvider.GetService<IPersonRepository>();

            try
            {
                _testPerson = (await _sut.GetPersonAsync(285)).Result;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsObject()
        {
            Assert.IsNotNull(_testPerson, _exception?.ToString());
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsId()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.Id > 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsName()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsNotNull(_testPerson.Name);
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.Title));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.FirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.MiddleName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.LastName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.Name.Suffix));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsPersonType()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsFalse(string.IsNullOrWhiteSpace(_testPerson.PersonType));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsEmailAddresses()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.EmailAddresses.Count != 0);
            Assert.IsFalse(_testPerson.EmailAddresses.Any(addr => addr.Id == null));
            Assert.IsFalse(_testPerson.EmailAddresses.Any(addr => string.IsNullOrWhiteSpace(addr.Address)));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsPhoneNumbers()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.PhoneNumbers.Count != 0);
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Number)));
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsAddresses()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.Addresses.Count != 0);
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Number)));
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
        }
    }

    [TestClass]
    public class SearchPersonTests
    {
        private static IPersonRepository? _sut;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            try
            {
                _sut = ServiceProvider.GetService<IPersonRepository>();
            }
            catch
            {
                _sut = null;
            }
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SearchPerson_WhenNoCriteria_Throws()
        {
            if (_sut == null)
                Assert.Inconclusive();

            _ = await _sut.SearchPersonsAsync(new PersonSearch(), 1);

            Assert.Fail();
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenEmailAddressSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { EmailAddress = "stephen0@adventure-works.com" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenFirstNameSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { FirstName = "Stephen" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenMiddleNameSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { MiddleName = "E" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenLastNameSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { LastName = "Carson" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenPersonTypeSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { PersonType = "SP" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenPhoneNumberSet_ReturnsFilteredResults()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { PhoneNumber = "2385550197" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count != 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenSearched_ReturnsTotal()
        {
            if (_sut == null)
                Assert.Inconclusive();

            PersonSearch criteria = new() { FirstName = "Stephen" };

            SearchResult<Person> results = await _sut.SearchPersonsAsync(criteria, 1);

            if (results.Results == null || results.Results.Count == 0)
                Assert.Inconclusive();

            Assert.IsTrue(results.Total > 0);
        }
    }
}