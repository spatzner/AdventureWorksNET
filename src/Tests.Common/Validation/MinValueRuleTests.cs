using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Domain.Validation
{
    [TestClass]
    public class MinValueRuleTests
    {
        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenNull_IsValid()
        {
            object? inputValue = null;
            var sut = new MinValueRule<int>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void MinValueRule_WhenTypeMismatch_Throws()
        {
            object? inputValue = 1.0m;
            var sut = new MinValueRule<int>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.Fail();
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenIntegerIsLessThanMin_IsNotValid()
        {
            int inputValue = 1;
            var sut = new MinValueRule<int>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenIntegerIsEqualToMinNotIncluded_IsNotValid()
        {
            int inputValue = 2;
            var sut = new MinValueRule<int>(2, minIncluded: false);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenIntegerIsEqualToMinIncluded_IsValid()
        {
            int inputValue = 2;
            var sut = new MinValueRule<int>(2, minIncluded: true);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenIntegerIsGreaterThanMin_IsValid()
        {
            int inputValue = 3;
            var sut = new MinValueRule<int>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenDecimalIsLessThanMin_IsNotValid()
        {
            decimal inputValue = 1;
            var sut = new MinValueRule<decimal>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenDecimalIsEqualToMinNotIncluded_IsNotValid()
        {
            decimal inputValue = 2;
            var sut = new MinValueRule<decimal>(2, minIncluded: false);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenDecimalIsEqualToMinIncluded_IsValid()
        {
            decimal inputValue = 2;
            var sut = new MinValueRule<decimal>(2, minIncluded: true);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenDecimalIsGreaterThanMin_IsValid()
        {
            decimal inputValue = 3;
            var sut = new MinValueRule<decimal>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinValueRule_WhenIsValid_ValidationErrorIsNull()
        {
            int inputValue = 3;
            var sut = new MinValueRule<int>(2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void MinLengthRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            string propertyName = "Name";
            int minValue = 2;
            int inputValue = 1;
            var sut = new MinValueRule<int>(minValue);

            bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.MinValue);
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){minValue}(?!=\d)"));
        }
    }
}