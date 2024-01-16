using System.Text.RegularExpressions;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class MaxLengthRuleTests
{
    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenNull_IsValid()
    {
        object? inputValue = null;
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenStringIsLessThanMax_IsValid()
    {
        object? inputValue = "a";
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenStringIsEqualToMax_IsValid()
    {
        object? inputValue = "ab";
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenStringIsGreaterThanMax_IsNotValid()
    {
        object? inputValue = "abc";
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenEnumerableIsLessThanMax_IsValid()
    {
        object? inputValue = new List<string> { "a" };
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenEnumerableIsEqualToMax_IsValid()
    {
        object? inputValue = new List<string> { "a", "b" };
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenEnumerableIGreaterThanMax_IsNotValid()
    {
        object? inputValue = new List<string> { "a", "b", "c" };
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void MaxLengthRule_WhenInvalidType_Throws()
    {
        object? inputValue = 1;
        MaxLengthRule sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.Fail();
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void MaxLengthRule_WhenIsValid_ValidationErrorIsNull()
    {
        object? inputValue = "a";
        MaxLengthRule sut = new(2);

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
        MaxLengthRule sut = new(maxLength);

        bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

        Assert.IsNotNull(result);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, propertyName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.MaxLength);
        Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){maxLength}(?!=\d)"));
    }
}