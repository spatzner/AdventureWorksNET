using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Common.Validation;
using Tests.Shared;

namespace Tests.Common.Validation;

[TestClass]
public class ValidationRuleTests
{
    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void ValidationRule_WhenExecuted_IsInvalidIsInverseOfIsValid()
    {
        object? inputValue = true;
        TestValidationRule sut = new();

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? _);
        bool isNotValid = sut.IsInvalid(string.Empty, inputValue, out ValidationError? _);

        Assert.AreEqual(isValid, !isNotValid);
    }

    private class TestValidationRule : ValidationRule
    {
        public override bool IsValid(string propertyName,
            object? value,
            [NotNullWhen(false)] out ValidationError? result)
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
            return new ValidationError
            {
                ValidationType = ValidationType.Required,
                Field = propertyName,
                Value = value,
                Requirements = "Value is required"
            };
        }
    }
}