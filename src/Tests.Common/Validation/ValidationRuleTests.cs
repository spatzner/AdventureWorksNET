using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using Tests.Shared;

namespace Tests.Common.Validation;

[TestClass]
public class ValidationRuleTests
{
    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void ValidationRule_WhenExecuted_IsInvalidIsInverseOfIsValid()
    {
        object? inputValue = true;
        var sut = new TestValidationRule();

        var isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);
        var isNotValid = sut.IsInvalid(string.Empty, inputValue, out ValidationError? result2);

        Assert.AreEqual(isValid, !isNotValid);
    }

    private class TestValidationRule : ValidationRule
    {
        public override bool IsValid(string propertyName, object? value, out ValidationError? result)
        {
            if (value == null)
            {
                result = GetErrorMessage(propertyName, value);
                return false;
            }

            bool isValid = (bool)value;
            result = isValid ? null : GetErrorMessage(propertyName, value);
            return isValid;
        }

        protected override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError()
            {
                ValidationType = ValidationType.Required,
                Field = propertyName,
                Value = value,
                Requirements = "Value is required"
            };
        }
    }
}