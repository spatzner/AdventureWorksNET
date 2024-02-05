using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Moq;

namespace Tests.Domain.Validation;

[TestClass]
public class AddressValidatorTests
{
    [TestMethod]
    public void Validate_WhenCalled_ExecutesCorrectValidations()
    {
        //Arrange
        List<string> currentRuleList = [];
        Dictionary<string, List<string>> callDictionary = new() { { "null", currentRuleList } };

        Address address = new();

        Mock<IValidationBuilder> mockValidationBuilder = new();

        mockValidationBuilder
           .Setup(x => x.NotNullOrEmptyRule())
           .Callback(() =>
            {
                List<string> newList = [];
                currentRuleList = callDictionary.TryAdd("NotNullOrEmptyRule()", newList)
                    ? newList
                    : callDictionary["NotNullOrEmptyRule()"];
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.Type)),
                It.Is<string>(s => s == nameof(Address.Type))))
           .Callback(() => currentRuleList.Add("Validate(Type)"))
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.Address1)),
                It.Is<string>(s => s == nameof(Address.Address1))))
           .Callback(() => currentRuleList.Add("Validate(Address1)"))
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.City)),
                It.Is<string>(s => s == nameof(Address.City))))
           .Callback(() => currentRuleList.Add("Validate(City)"))
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.State)),
                It.Is<string>(s => s == nameof(Address.State))))
           .Callback(() => currentRuleList.Add("Validate(State)"))
           .Returns(mockValidationBuilder.Object);
        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.Country)),
                It.Is<string>(s => s == nameof(Address.Country))))
           .Callback(() => currentRuleList.Add("Validate(Country)"))
           .Returns(mockValidationBuilder.Object);
        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, address.PostalCode)),
                It.Is<string>(s => s == nameof(Address.PostalCode))))
           .Callback(() => currentRuleList.Add("Validate(PostalCode)"))
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder.Setup(x => x.GetResult()).Returns(new ValidationResult());

        AddressValidator sut = new(mockValidationBuilder.Object);

        //Act
        ValidationResult result = sut.Validate(address);

        //Assert
        Assert.AreEqual(callDictionary.Count(x => x.Key != "null"), 1, "Additional ValidationRules were called that should not have been");

        if (callDictionary.TryGetValue("null", out List<string>? nullList))
            Assert.IsTrue(nullList.Count == 0, "Validate call(s) made without a validator");

        if (!callDictionary.TryGetValue("NotNullOrEmptyRule()", out List<string>? requiredList))
            Assert.Fail("NotNullOrEmptyRule() not called");

        Assert.AreEqual(requiredList.Count, 6);
        Assert.IsTrue(requiredList.Contains("Validate(Type)"), "Type not validated on NotNullOrEmptyRule");
        Assert.IsTrue(requiredList.Contains("Validate(Address1)"), "Address1 not validated for NotNullOrEmptyRule");
        Assert.IsTrue(requiredList.Contains("Validate(City)"), "City not validated on NotNullOrEmptyRule");
        Assert.IsTrue(requiredList.Contains("Validate(State)"), "State not validated on NotNullOrEmptyRule");
        Assert.IsTrue(requiredList.Contains("Validate(Country)"), "Country not validated on NotNullOrEmptyRule");
        Assert.IsTrue(requiredList.Contains("Validate(PostalCode)"), "PostalCode not validated on NotNullOrEmptyRule");

        mockValidationBuilder.Verify(x => x.GetResult(), Times.Once);
    }
    
    
}