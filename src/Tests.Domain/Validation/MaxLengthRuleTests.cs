using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;

namespace Tests.Domain.Validation
{
    [TestClass]
    public class MaxLengthRuleTests
    {
        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenNull_IsValid()
        {
            object? inputValue = null;
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenStringIsLessThanMax_IsValid()
        {
            object? inputValue = "a";
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenStringIsEqualToMax_IsValid()
        {
            object? inputValue = "ab";
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenStringIsGreaterThanMax_IsNotValid()
        {
            object? inputValue = "abc";
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenEnumerableIsLessThanMax_IsValid()
        {
            object? inputValue = new List<string> { "a" };
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenEnumerableIsEqualToMax_IsValid()
        {
            object? inputValue = new List<string> { "a","b" };
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenEnumerableIGreaterThanMax_IsNotValid()
        {
            object? inputValue = new List<string> { "a", "b", "c" };
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void MaxLengthRule_WhenInvalidType_Throws()
        {
            object? inputValue = 1;
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenIsValid_ValidationErrorIsNull()
        {
            object? inputValue = "a";
            var sut = new MaxLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MaxLengthRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            string propertyName = "Name";
            object? inputValue = "abc";
            int maxLength = 2;
            var sut = new MaxLengthRule(maxLength);

            bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.MaxLength);
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){maxLength}(?!=\d)"));
        }

        
    }
}
