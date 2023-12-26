using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using System.Text.RegularExpressions;

namespace Tests.Domain.Validation
{
    [TestClass]
    public class MinLengthRuleTests
    {
        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_WhenNull_IsValid()
        {

            object? inputValue = null;
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_StringLessThanMin_IsNotValid()
        {

            object? inputValue = "a";
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_StringEqualToMin_IsValid()
        {

            object? inputValue = "ab";
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_StringGreaterThanMin_IsValid()
        {

            object? inputValue = "abc";
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_EnumerableLessThanMin_IsNotValid()
        {

            object? inputValue = new List<string> { "a" };
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_EnumerableEqualToMin_IsValid()
        {

            object? inputValue = new List<string> { "a", "b" };
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_EnumerableGreaterThanMin_IsValid()
        {
            object? inputValue = new List<string> { "a", "b", "c" };
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void MinLengthRule_WhenInvalidType_Throws()
        {
            object? inputValue = 1;
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_WhenIsValid_ValidationErrorIsNull()
        {
            object? inputValue = "abc";
            var sut = new MinLengthRule(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            string propertyName = "Name";
            object? inputValue = "a";
            int minLength = 2;
            var sut = new MinLengthRule(minLength);

            bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.MinLength);
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){minLength}(?!=\d)", RegexOptions.None));
        }
    }
}
