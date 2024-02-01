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
        List<string> currentRuleList = new();
        Dictionary<string, List<string>> callDictionary = new() { { "null", currentRuleList } };

        Address address = new();

        Mock<IValidationBuilder> mockValidationBuilder = new();

        mockValidationBuilder
           .Setup(x => x.RequiredRule())
           .Callback(() =>
            {
                List<string> newList = new();
                currentRuleList = callDictionary.TryAdd("RequiredRule()", newList)
                    ? newList
                    : callDictionary["RequiredRule()"];
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
        Assert.AreEqual(callDictionary.Count, 2, "Additional ValidationRules were called that should not have been");

        if (callDictionary.TryGetValue("null", out List<string>? nullList))
            Assert.IsFalse(nullList.Any());

        if (!callDictionary.TryGetValue("RequiredRule()", out List<string>? requiredList))
            Assert.Fail("RequiredRule() not called");

        Assert.AreEqual(requiredList.Count, 6);
        Assert.IsTrue(requiredList.Contains("Validate(Type)"));
        Assert.IsTrue(requiredList.Contains("Validate(Address1)"));
        Assert.IsTrue(requiredList.Contains("Validate(City)"));
        Assert.IsTrue(requiredList.Contains("Validate(State)"));
        Assert.IsTrue(requiredList.Contains("Validate(Country)"));
        Assert.IsTrue(requiredList.Contains("Validate(PostalCode)"));

        mockValidationBuilder.Verify(x => x.GetResult(), Times.Once);
    }
}