using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Moq;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class AddressValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAllValidationsMet_IsValid()
    {
        Address address = GetValidAddress();

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenTypeIsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.Type = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAddress1IsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.Address1 = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenCityIsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.City = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenStateIsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.State = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenCountryIsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.Country = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenPostalCodeIsEmpty_IsInValid()
    {
        Address address = GetValidAddress();
        address.PostalCode = string.Empty;

        var sut = new AddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    private Address GetValidAddress()
    {
        return new Address
        {
            Type = "Home",
            Address1 = "123 Main St",
            City = "Anytown",
            State = "WA",
            Country = "USA",
            PostalCode = "12345"
        };
    }
}