using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;
using Microsoft.VisualBasic;

namespace Tests.Domain.Validation
{
    [TestClass]
    public class DiscreetValueRuleTests
    {
        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenNull_IsValid()
        {
            object? inputValue = null;
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenValueIsWrongType_IsNotValid()
        {
            object? inputValue = "1";
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenValueIsNotInList_IsNotValid()
        {
            object? inputValue = 3;
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenValueIsInList_IsValid()
        {
            object? inputValue = 1;
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenIsValid_ValidationErrorIsNull()
        {
            object? inputValue = 1;
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void DiscreetValueRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            object? inputValue = 3;
            string propertyName = "Name";
            var sut = new DiscreetValueRule<int>(1, 2);

            bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.DiscreetValue);
            Assert.IsTrue(result.Requirements.Contains("'1'") && result.Requirements.Contains("'2'"));

        }
    }
}
