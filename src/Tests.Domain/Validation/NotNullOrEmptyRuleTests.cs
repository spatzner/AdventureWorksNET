using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace Tests.Domain.Validation
{
    [TestClass]
    public class NotNullOrEmptyRuleTests
    {

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenNull_IsNotValid()
        {
            object? inputValue = null;
            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenStringIsEmpty_IsNotValid()
        {
            object? inputValue = string.Empty;
            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenStringHasValue_IsValid()
        {
            object? inputValue = "b";
            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenEnumerableIsEmpty_IsNotValid()
        {
            object? inputValue = new List<string>();
            
            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenEnumerableHasElements_IsValid()
        {
            object? inputValue = new List<string> { "b" };

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void NotNullOrEmptyRule_WhenValueIsReferenceType_Throws()
        {
            object? inputValue = 1;

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassHasNoPropertiesSet_IsNotValid()
        {
            object? inputValue = new ValuesClass();

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassRefTypeSet_IsValid()
        {
            object? inputValue = new ValuesClass
            {
                RefType = "test"
            };

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassNullRefTypeSet_IsValid()
        {
            object? inputValue = new ValuesClass
            {
                NullRefType = "test"
            };

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassValueTypeSet_IsValid()
        {
            object? inputValue = new ValuesClass
            {
                ValueType = 1
            };

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassNullValueTypeSet_IsValid()
        {
            object? inputValue = new ValuesClass
            {
                NullValueType = 0
            };

            string propName = "name";

            var isValid = new NotNullOrEmptyRule().IsValid(propName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenIsValid_ValidationErrorIsNull()
        {

            string inputValue = "valid";
            var sut = new NotNullOrEmptyRule();

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            string inputValue = string.Empty;
            string propertyName = "Name";

            var sut = new NotNullOrEmptyRule();

            bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);


            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.IsNotEmpty);
            Assert.AreEqual(result.Requirements, string.Empty);
        }

        private class ValuesClass
        {
            public string RefType { get; set; }
            public string? NullRefType { get; set; }
            public int ValueType{ get; set; }
            public int? NullValueType { get; set; }
        }

    }
}
