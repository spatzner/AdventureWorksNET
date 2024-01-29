using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;

namespace Tests.Common.Validation;

[TestClass]
public class ValidationBuilderTests
{
    //using the solution's unit tests as a guide, create unit tests for the AdventureWorks.Common.Validation.ValidationBuilder class    //using the solution's unit tests as a guide, create unit tests for the AdventureWorks.Common.Validation.ValidationBuilder class.
    [TestMethod]
    public void DiscreetValueRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();

        var result = sut.DiscreetValueRule(1, 2, 3).Validate(1, "test").GetResult();

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    public void DiscreetValueRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();

        var result = sut.DiscreetValueRule(1, 2, 3).Validate(4, "test").GetResult();

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void MaxLengthRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();

        var result = sut.MaxLengthRule(5).Validate("test", "test").GetResult();

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    public void MaxLengthRule_WithInvalidValue_ReturnsFalse()
    {
        //Arrange
        var sut = new ValidationBuilder();

        var result = sut.MaxLengthRule(5).Validate("test123", "test").GetResult();

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void MinLengthRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();

        var result = sut.MinLengthRule(5).Validate("test123", "test").GetResult();

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    public void MinLengthRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();

        var result = sut.MinLengthRule(5).Validate("test", "test").GetResult();

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void NotNullOrEmptyRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();

        var result = sut.NotNullOrEmptyRule().Validate("test", "test").GetResult();

        Assert.IsTrue(result.IsValidRequest);
    }

    //write the rest of the unit tests for this class    //write the rest of the unit tests for this class
    [TestMethod]
    public void NotNullOrEmptyRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();
        var result = sut.NotNullOrEmptyRule().Validate("", "test").GetResult();
        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void RangeRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();
        var result = sut.RangeRule(1, 10).Validate(5, "test").GetResult();
        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    public void RangeRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();
        var result = sut.RangeRule(1, 10).Validate(11, "test").GetResult();
        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void RequiredRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();
        var result = sut.RequiredRule().Validate("test", "test").GetResult();
        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    public void RequiredRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();
        var result = sut.RequiredRule().Validate(null, "test").GetResult();
        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    public void UniqueOnRule_WithValidValue_ReturnsTrue()
    {
        var sut = new ValidationBuilder();

        var data = new List<UniqueOnRuleTest>
        {
            new() { Id = 1, Name = string.Empty }, new() { Id = 2, Name = string.Empty }
        };

        new UniqueOnRule<UniqueOnRuleTest>(x => x.Id).IsValid("string", data, out ValidationError? r);
        Assert.IsTrue(r == null);

        //var result =  sut.UniqueOnRule<UniqueOnRuleTest>(x => x.Id).Validate(data, "test").GetResult();
        //Assert.IsTrue(result);
    }

    [TestMethod]
    public void UniqueOnRule_WithInvalidValue_ReturnsFalse()
    {
        var sut = new ValidationBuilder();

        var data = new List<UniqueOnRuleTest>
        {
            new() { Id = 1, Name = string.Empty }, new() { Id = 1, Name = string.Empty }
        };

        var result = sut.UniqueOnRule<UniqueOnRuleTest>(x => new { x.Id }).Validate(data, "test").GetResult();
        Assert.IsFalse(result.IsValidRequest);
    }

    public class MyClass
    {
        public int MyProperty { get; set; }
    }

    private class UniqueOnRuleTest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}