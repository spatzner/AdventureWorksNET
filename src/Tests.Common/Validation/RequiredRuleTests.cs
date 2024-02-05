using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class RequiredRuleTests
{
    private readonly string _propertyName = "name";

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void RequiredRule_WhenIsNull_IsNotValid()
    {
        object? inputValue = null;
        bool isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void RequiredRule_WhenIsNotNull_IsValid()
    {
        object? inputValue = "value";
        bool isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void RequiredRule_WhenIsValid_ValidationErrorIsNull()
    {
        object? inputValue = "value";

        bool isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void RequiredRule_WhenIsNotValid_ValidationErrorIsCorrect()
    {
        object? inputValue = null;

        bool isValid = new RequiredRule().IsValid(_propertyName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, _propertyName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.Required);
        Assert.AreEqual(result.Requirements, string.Empty);
    }
}