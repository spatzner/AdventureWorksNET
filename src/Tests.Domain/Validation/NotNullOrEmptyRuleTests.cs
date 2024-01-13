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
// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Domain.Validation
{
    [TestClass]
    public class NotNullOrEmptyRuleTests
    {
        private const string PropName = "name";

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenNull_IsNotValid()
        {
            object? inputValue = null;

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenStringIsEmpty_IsNotValid()
        {
            object? inputValue = string.Empty;

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenStringHasValue_IsValid()
        {
            object? inputValue = "b";

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenEnumerableIsEmpty_IsNotValid()
        {
            object? inputValue = new List<string>();

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenEnumerableHasElements_IsValid()
        {
            object? inputValue = new List<string> { "b" };

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void NotNullOrEmptyRule_WhenValueIsReferenceType_Throws()
        {
            object? inputValue = 1;

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

            Assert.Fail();
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void NotNullOrEmptyRule_WhenClassHasNoPropertiesSet_IsNotValid()
        {
            object? inputValue = new ValuesClass();

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

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

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

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

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

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

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

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

            var isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

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

            var sut = new NotNullOrEmptyRule();

            bool isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);


            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, PropName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.IsNotEmpty);
            Assert.AreEqual(result.Requirements, string.Empty);
        }

        private class ValuesClass
        {
            public string RefType { get; set; } = null!;
            public string? NullRefType { get; set; }
            public int ValueType{ get; set; }
            public int? NullValueType { get; set; }
        }

    }
}
