using AdventureWorks.Common.Validation;
using Tests.Shared;

// ReSharper disable UnusedVariable
#pragma warning disable IDE0059

namespace Tests.Common.Validation;

[TestClass]
public class NotNullOrEmptyRuleTests
{
    private const string PropName = "name";

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenNull_IsNotValid()
    {
        object? inputValue = null;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenStringIsEmpty_IsNotValid()
    {
        object? inputValue = string.Empty;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenStringHasValue_IsValid()
    {
        object? inputValue = "b";

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenEnumerableIsEmpty_IsNotValid()
    {
        object? inputValue = new List<string>();

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenEnumerableHasElements_IsValid()
    {
        object? inputValue = new List<string> { "b" };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void NotNullOrEmptyRule_WhenValueIsReferenceType_Throws()
    {
        object? inputValue = 1;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.Fail();
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenClassHasNoPropertiesSet_IsNotValid()
    {
        object? inputValue = new ValuesClass();

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenClassRefTypeSet_IsValid()
    {
        object? inputValue = new ValuesClass { RefType = "test" };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenClassNullRefTypeSet_IsValid()
    {
        object? inputValue = new ValuesClass { NullRefType = "test" };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenClassValueTypeSet_IsValid()
    {
        object? inputValue = new ValuesClass { ValueType = 1 };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenClassNullValueTypeSet_IsValid()
    {
        object? inputValue = new ValuesClass { NullValueType = 0 };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenIsValid_ValidationErrorIsNull()
    {
        string inputValue = "valid";
        NotNullOrEmptyRule sut = new();

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void NotNullOrEmptyRule_WhenIsNotValid_ValidationErrorIsCorrect()
    {
        string inputValue = string.Empty;

        NotNullOrEmptyRule sut = new();

        bool isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, PropName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.IsNotEmpty);
        Assert.AreEqual(result.Requirements, string.Empty);
    }

    private class ValuesClass
    {
        public string RefType { get; set; } = null!;
        public string? NullRefType { get; set; }
        public int ValueType { get; set; }
        public int? NullValueType { get; set; }
    }
}