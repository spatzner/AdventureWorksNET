using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class GeoPointValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenOriginPoint_IsValid()
    {
        var getPoint = new GeoPoint(0, 0);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLatitudeLessThanNeg90_IsNotValid()
    {
        var getPoint = new GeoPoint(-91, 0);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLatitudeIsNeg90_IsValid()
    {
        var getPoint = new GeoPoint(-90, 0);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLatitudeMoreThan90_IsNotValid()
    {
        var getPoint = new GeoPoint(91, 0);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLatitudeIs90_IsValid()
    {
        var getPoint = new GeoPoint(90, 0);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLongitudeLessThanNeg180_IsNotValid()
    {
        var getPoint = new GeoPoint(0, -181);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLongitudeIsNeg180_IsValid()
    {
        var getPoint = new GeoPoint(0, -180);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLongitudeMoreThan180_IsNotValid()
    {
        var getPoint = new GeoPoint(0, 181);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenLongitudeIs180_IsValid()
    {
        var getPoint = new GeoPoint(0, 180);

        var sut = new GeoPointValidator(new ValidationBuilder());

        var result = sut.Validate(getPoint);

        Assert.IsTrue(result.IsValidRequest);
    }
}
