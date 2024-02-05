using System.Text.RegularExpressions;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class MinValueRuleTests
{
    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenNull_IsValid()
    {
        object? inputValue = null;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueRule_WhenTypeMismatch_Throws()
    {
        object? inputValue = 1.0m;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.Fail();
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentNullException))]
    public void MinValueRuleCtor_WhenValueIsNull_Throws()
    {
        var rule = new MinValueRule<int?>(null);
    }
    
    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueRuleCtor_WhenValueIsNotDecimalOrInt_Throws()
    {
        var rule = new MinValueRule<string>("test");
    }
    
    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueRule_IsValid_WhenValueIsNotDecimalOrInt_Throws()
    {
        // Arrange
        var rule = new MinValueRule<int>(1);
        string value = "test";
        // Act
        var isValid = rule.IsValid(string.Empty, value, out var result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenIntegerIsLessThanMin_IsNotValid()
    {
        int inputValue = 1;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenIntegerIsEqualToMinNotIncluded_IsNotValid()
    {
        int inputValue = 2;
        MinValueRule<int> sut = new(2, false);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenIntegerIsEqualToMinIncluded_IsValid()
    {
        int inputValue = 2;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenIntegerIsGreaterThanMin_IsValid()
    {
        int inputValue = 3;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenDecimalIsLessThanMin_IsNotValid()
    {
        decimal inputValue = 1;
        MinValueRule<decimal> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenDecimalIsEqualToMinNotIncluded_IsNotValid()
    {
        decimal inputValue = 2;
        MinValueRule<decimal> sut = new(2, false);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenDecimalIsEqualToMinIncluded_IsValid()
    {
        decimal inputValue = 2;
        MinValueRule<decimal> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenDecimalIsGreaterThanMin_IsValid()
    {
        decimal inputValue = 3;
        MinValueRule<decimal> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinValueRule_WhenIsValid_ValidationErrorIsNull()
    {
        int inputValue = 3;
        MinValueRule<int> sut = new(2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void MinLengthRule_WhenIsNotValid_ValidationErrorIsCorrect()
    {
        string propertyName = "Name";
        int minValue = 2;
        int inputValue = 1;
        MinValueRule<int> sut = new(minValue);

        bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, propertyName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.MinValue);
        Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){minValue}(?!=\d)"));
    }
}