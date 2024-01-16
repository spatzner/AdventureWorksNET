using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Domain.Validation
{
    [TestClass]
    public class RequiredRuleTests
    {
        private readonly string _propertyName = "name";

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RequiredRule_WhenIsNull_IsNotValid()
        {
            object? inputValue = null;
            var isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RequiredRule_WhenIsNotNull_IsValid()
        {
            object? inputValue = "value";
            var isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RequiredRule_WhenIsValid_ValidationErrorIsNull()
        {
            object? inputValue = "value";

            var isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RequiredRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            object? inputValue = null;

            var isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, _propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.Required);
            Assert.AreEqual(result.Requirements, string.Empty);
        }
    }
}
