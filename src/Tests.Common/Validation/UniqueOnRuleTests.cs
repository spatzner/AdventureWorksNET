using AdventureWorks.Common.Validation;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class UniqueOnRuleTests
{
    public const string PropName = "name";

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void UniqueRuleCtor_WhenBodyTypeNotMemberOrNewExpression_Throws()
    {
        var sut = new UniqueOnRule<string>(s => s + s);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void UniqueRuleCtor_WhenNoMembersProvided_Throws()
    {
        var sut = new UniqueOnRule<string>(s => new { });
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenNull_IsValid()
    {
        object? inputValue = null;

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void UniqueRule_WhenNotEnumerable_Throws()
    {
        object? inputValue = 3;

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    [ExpectedException(typeof(ArgumentException))]
    public void UniqueRule_WhenListTypeMismatchWithClassType_Throws()
    {
        object? inputValue = new List<int>();

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenSingleKeyPropertyWithMatch_IsNotValid()
    {
        object? inputValue = new List<TestClass> { new() { Value1 = "A" }, new() { Value1 = "A" } };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenMultipleKeyPropertiesWithMatch_IsNotValid()
    {
        object? inputValue = new List<TestClass>
        {
            new() { Value1 = "A", Value2 = "B" }, new() { Value1 = "A", Value2 = "B" }
        };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1, tc.Value2 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenMatchOnKeysDifferentOnNonKey_IsNotValid()
    {
        object? inputValue = new List<TestClass>
        {
            new() { Value1 = "A", Value2 = "B", Value3 = "1" }, new() { Value1 = "A", Value2 = "B", Value3 = "2" }
        };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1, tc.Value2 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenSomeMatchOnKeys_IsNotValid()
    {
        object? inputValue = new List<TestClass>
        {
            new() { Value1 = "1", Value2 = "2" },
            new() { Value1 = "A", Value2 = "B" },
            new() { Value1 = "A", Value2 = "B" }
        };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1, tc.Value2 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenAllUniqueOnKeys_IsValid()
    {
        object? inputValue = new List<TestClass>
        {
            new() { Value1 = "1", Value2 = "2" },
            new() { Value1 = "A", Value2 = "B" },
            new() { Value1 = "Y", Value2 = "Z" }
        };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1, tc.Value2 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenIsValid_ValidationErrorIsNull()
    {
        object? inputValue = new List<TestClass> { new() { Value1 = "A" }, new() { Value1 = "1" } };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenIsNotValid_ValidationErrorIsNull()
    {
        object? inputValue = new List<TestClass> { new() { Value1 = "A" }, new() { Value1 = "1" } };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsTrue(isValid);
        Assert.IsNull(result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void UniqueRule_WhenIsNotValid_ValidationErrorIsCorrect()
    {
        object? inputValue = new List<TestClass>
        {
            new() { Value1 = "A", Value2 = "B" }, new() { Value1 = "A", Value2 = "B" }
        };

        var sut = new UniqueOnRule<TestClass>(tc => new { tc.Value1, tc.Value2 });

        var isValid = sut.IsValid(PropName, inputValue, out ValidationError? result);

        Assert.IsFalse(isValid);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Field, PropName);
        Assert.AreEqual(result.Value, inputValue);
        Assert.AreEqual(result.ValidationType, ValidationType.UniqueList);
        Assert.IsTrue(result.Requirements.Contains(nameof(TestClass.Value1)));
        Assert.IsTrue(result.Requirements.Contains(nameof(TestClass.Value2)));
        Assert.IsFalse(result.Requirements.Contains(nameof(TestClass.Value3)));
    }

    private class TestClass
    {
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
    }
}