using System.Text.RegularExpressions;
using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class DiscreetValueRuleTests
{
    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenNull_IsValid()
    {
        object? inputValue = null;
        DiscreetValueRule<int> sut = new(1, 2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenValueIsWrongType_IsNotValid()
    {
        object? inputValue = "1";
        DiscreetValueRule<int> sut = new(1, 2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenValueIsNotInList_IsNotValid()
    {
        object? inputValue = 3;
        DiscreetValueRule<int> sut = new(1, 2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenValueIsInList_IsValid()
    {
        object? inputValue = 1;
        DiscreetValueRule<int> sut = new(1, 2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenIsValid_ValidationErrorIsNull()
    {
        object? inputValue = 1;
        DiscreetValueRule<int> sut = new(1, 2);

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void DiscreetValueRule_WhenIsNotValid_ValidationErrorIsCorrect()
    {
        object? inputValue = 3;
        string propertyName = "Name";
        int[] discreetValues = [1, 2];
        DiscreetValueRule<int> sut = new(discreetValues);

        bool isValid = sut.IsValid(propertyName, inputValue, out ValidationError? result);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, propertyName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.DiscreetValue);
        foreach (int discreetValue in discreetValues)
            Assert.IsTrue(Regex.IsMatch(result.Requirements, $@"(?<!\d){discreetValue}(?!=\d)"));
    }
}