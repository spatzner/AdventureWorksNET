using System.Text.RegularExpressions;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class RangeRuleTests
{
    private readonly string _propertyName = "name";

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenNull_IsValid()
    {
        object? inputValue = null;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void RangeRule_WhenIsNotIntegralOrDecimal_Throws()
    {
        object? inputValue = "3";

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.Fail();
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenLessThanMin_IsNotValid()
    {
        object? inputValue = 1;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenMinAndMinInclusive_IsValid()
    {
        object? inputValue = 2;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenMinInclusiveNotProvided_IsInclusive()
    {
        object? inputValue = 2;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenMinAndNotMinInclusive_IsNotValid()
    {
        object? inputValue = 2;

        bool isValid = new RangeRule(2, 4, false).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenBetweenMinAndMax_IsValid()
    {
        object? inputValue = 3;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenMaxAndMaxInclusive_IsValid()
    {
        object? inputValue = 4;

        bool isValid =
            new RangeRule(2, 4, maxInclusive: true).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenMaxAndNotMaxInclusive_IsNotValid()
    {
        object? inputValue = 4;

        bool isValid =
            new RangeRule(2, 4, maxInclusive: false).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenNotMaxInclusiveNotProvided_IsMaxInclusive()
    {
        object? inputValue = 4;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenGreaterThanMax_IsNotValid()
    {
        object? inputValue = 5;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void RangeRule_WhenIsValid_ValidationErrorIsNull()
    {
        object? inputValue = 3;

        bool isValid = new RangeRule(2, 4).IsValid(_propertyName, inputValue, out ValidationError? result);

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

        bool isValid =
            new RangeRule(minValue, maxValue).IsValid(_propertyName, inputValue, out ValidationError? result);

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

        bool isValid =
            new RangeRule(minValue, maxValue).IsValid(_propertyName, inputValue, out ValidationError? result);

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

        bool isValid =
            new RangeRule(minValue, maxValue, maxInclusive: true).IsValid(_propertyName,
                inputValue,
                out ValidationError? result);

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

        bool isValid =
            new RangeRule(minValue, maxValue, false).IsValid(_propertyName, inputValue, out ValidationError? result);

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

        bool isValid =
            new RangeRule(minValue, maxValue, maxInclusive: false).IsValid(_propertyName,
                inputValue,
                out ValidationError? result);

        Assert.IsFalse(isValid);
        Assert.IsTrue(result!.Requirements.Contains($"value < {maxValue}"));
    }
}