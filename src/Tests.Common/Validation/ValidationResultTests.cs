using System.Security.Cryptography.X509Certificates;
using AdventureWorks.Common.Validation;
using Tests.Shared;

namespace Tests.Common.Validation;

[TestClass]
public class ValidationResultTests
{
    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void Errors_WhenNoErrorsAreAdded_IsInitializedWithEmptyCollection()
    {
        ValidationResult sut = new();
        List<ValidationError> result = sut.Errors;
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void IsValidRequest_WhenNoErrorsAreAdded_IsTrue()
    {
        ValidationResult sut = new();
        bool result = sut.IsValidRequest;
        Assert.IsTrue(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void IsValidRequest_WhenErrorsAreAdded_IsFalse()
    {
        ValidationResult sut = new();
        sut.Errors.Add(new ValidationError
        {
            Field = "Field", ValidationType = ValidationType.Required, Requirements = string.Empty
        });
        bool result = sut.IsValidRequest;
        Assert.IsFalse(result);
    }

    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void Constructor_WhenErrorsArePassed_SetsErrors()
    {
        ValidationError error = new()
        {
            Field = "Field", ValidationType = ValidationType.Required, Requirements = string.Empty
        };

        ValidationResult validationResult = new();
        validationResult.Errors.Add(error);

        TestValidationResult sut = new(validationResult);

        Assert.AreEqual(1, sut.Errors.Count);
    }

    class TestValidationResult(ValidationResult validationResult) : ValidationResult(validationResult);
}