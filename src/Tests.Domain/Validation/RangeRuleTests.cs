﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Validation;
// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Domain.Validation
{
    [TestClass]
    public class RangeRuleTests
    {

        private readonly string _propertyName = "name";

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenNull_IsValid()
        {
            object? inputValue = null;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RangeRule_WhenIsNotIntegralOrDecimal_Throws()
        {
            object? inputValue = "3";

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.Fail();
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenLessThanMin_IsNotValid()
        {
            object? inputValue = 1;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMinAndMinInclusive_IsValid()
        {
            object? inputValue = 2;

            var isValid = new RangeRule(2, 4, minInclusive:true).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMinInclusiveNotProvided_IsInclusive()
        {
            object? inputValue = 2;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMinAndNotMinInclusive_IsNotValid()
        {
            object? inputValue = 2;

            var isValid = new RangeRule(2, 4, minInclusive: false).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenBetweenMinAndMax_IsValid()
        {
            object? inputValue = 3;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMaxAndMaxInclusive_IsValid()
        {
            object? inputValue = 4;

            var isValid = new RangeRule(2, 4, maxInclusive: true).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMaxAndNotMaxInclusive_IsNotValid()
        {
            object? inputValue = 4;

            var isValid = new RangeRule(2, 4, maxInclusive: false).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenNotMaxInclusiveNotProvided_IsMaxInclusive()
        {
            object? inputValue = 4;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenGreaterThanMax_IsNotValid()
        {
            object? inputValue = 5;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenIsValid_ValidationErrorIsNull()
        {
            object? inputValue = 3;

            var isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsTrue(isValid);
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenIsNotValid_ValidationErrorIsCorrect()
        {
            object? inputValue = 5;
            int minValue = 2;
            int maxValue = 4;

            var isValid = new RangeRule(minValue, maxValue).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, _propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.Range);
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){minValue}(?!=\d)"));
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){maxValue}(?!=\d)"));
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMinInclusiveAndIsNotValid_ValidationErrorRequirementCorrect()
        {
            object? inputValue = 5;
            int minValue = 2;
            int maxValue = 4;

            var isValid = new RangeRule(minValue, maxValue, minInclusive:true).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, _propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.Range);
            Assert.IsTrue(result.Requirements.Contains($"{minValue} <= value"));
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenMaxInclusiveAndIsNotValid_ValidationErrorRequirementCorrect()
        {
            object? inputValue = 5;
            int minValue = 2;
            int maxValue = 4;

            var isValid = new RangeRule(minValue, maxValue, maxInclusive:true).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Field, _propertyName);
            Assert.AreEqual(result.Value, inputValue);
            Assert.AreEqual(result.ValidationType, ValidationType.Range);
            Assert.IsTrue(result.Requirements.Contains($"value <= {maxValue}"));
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenNotMinInclusiveAndIsNotValid_ValidationErrorRequirementCorrect()
        {
            object? inputValue = 5;
            int minValue = 2;
            int maxValue = 4;

            var isValid = new RangeRule(minValue, maxValue, minInclusive: false).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsTrue(result!.Requirements.Contains($"{minValue} < value"));
        }

        [TestMethod]
        [TestCategory(Constants.Unit)]
        public void RangeRule_WhenNotMaxInclusiveAndIsNotValid_ValidationErrorRequirementCorrect()
        {
            object? inputValue = 5;
            int minValue = 2;
            int maxValue = 4;

            var isValid = new RangeRule(minValue, maxValue, maxInclusive: false).IsValid(_propertyName, inputValue, out ValidationError? result);

            Assert.IsFalse(isValid);
            Assert.IsTrue(result!.Requirements.Contains($"value < {maxValue}"));
        }


    }
}
