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
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenNull_IsNotValid()
    {
        object? inputValue = null;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenStringIsEmpty_IsNotValid()
    {
        object? inputValue = string.Empty;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenStringHasValue_IsValid()
    {
        object? inputValue = "b";

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenEnumerableIsEmpty_IsNotValid()
    {
        object? inputValue = new List<string>();

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenEnumerableHasElements_IsValid()
    {
        object? inputValue = new List<string> { "b" };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void NotNullOrEmptyRule_WhenValueIsReferenceType_Throws()
    {
        object? inputValue = 1;

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.Fail();
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenClassHasNoPropertiesSet_IsNotValid()
    {
        object? inputValue = new ReferenceClass();

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenClassRefTypeSet_IsValid()
    {
        object? inputValue = new ValidationClass { RefType = new ReferenceClass() };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenClassNullRefTypeSet_IsValid()
    {
        object? inputValue = new ValidationClass { NullableRefType = new() };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void NotNullOrEmptyRule_WhenClassHasValueType_Throws()
    {
        object? inputValue = new IllegalValidationClass { ValueType = 1 };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenClassNullValueTypeSet_IsValid()
    {
        object? inputValue = new ValidationClass { NullValueType = 0 };

        bool isValid = new NotNullOrEmptyRule().IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void NotNullOrEmptyRule_WhenIsValid_ValidationErrorIsNull()
    {
        string inputValue = "valid";
        NotNullOrEmptyRule sut = new();

        bool isValid = sut.IsValid(string.Empty, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
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

    private class ValidationClass
    {
        public ReferenceClass RefType { get; set; } = null!;
        public ReferenceClass? NullableRefType { get; set; }
        public int? NullValueType { get; set; }
    }

    private class IllegalValidationClass
    {
        public int? NullValueType { get; set; }
        public int ValueType { get; set; }
    }

    private class ReferenceClass
    {
        public string? SomeProperty { get; set; }
    }
}