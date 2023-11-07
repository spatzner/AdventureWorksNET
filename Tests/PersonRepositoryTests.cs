using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.SqlRepository;

namespace Tests;

public class PersonRepositoryTests
{

    [TestClass]
    public class GetPersonTests
    {

        private static PersonRepository? _sut;
        private static PersonDetail? _testPerson;
        private static Exception? _exception;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            _sut = new PersonRepository(Settings.ConnectionString);

            try
            {
                _testPerson = await _sut.Get(291);
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
            if(_testPerson == null)
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.Id > 0);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsName()
        {
            if (_testPerson == null)
                Assert.Inconclusive();

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

            Assert.IsTrue(_testPerson.EmailAddresses.Any());
            Assert.IsFalse(_testPerson.EmailAddresses.Any(string.IsNullOrWhiteSpace));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsPhoneNumbers()
        {
            if (_testPerson == null) 
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.PhoneNumbers.Any());
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Number)));
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public void GetPerson_WhenExists_ReturnsAddresses()
        {
            if (_testPerson == null) 
                Assert.Inconclusive();

            Assert.IsTrue(_testPerson.Addresses.Any());
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Number)));
            Assert.IsFalse(_testPerson.PhoneNumbers.Any(x => string.IsNullOrWhiteSpace(x.Type)));
        }
    }

    [TestClass]
    public class SearchPersonTests
    {
        private static PersonRepository? _sut;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _sut = new PersonRepository(Settings.ConnectionString);
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SearchPerson_WhenNoCriteria_Throws()
        {
            _ = await _sut!.Search(new PersonSearch());
        }

        [TestMethod]
        [TestCategory(Constants.Integration)]
        public async Task SearchPerson_WhenEmailAddressSet_ReturnsFilteredResults()
        {
            var criteria = new PersonSearch
            {
                EmailAddress = "stephen0@adventure-works.com"
            };

            var results = await _sut!.Search(criteria);

            Assert.AreEqual(results.Count, 1);

        }
    }
}


