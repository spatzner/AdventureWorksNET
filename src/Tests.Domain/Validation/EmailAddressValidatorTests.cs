using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Moq;

namespace Tests.Domain.Validation;
[TestClass]
public class EmailAddressValidatorTests
{
    [TestMethod]
    public void Validate_WhenCalled_ExecutesCorrectValidations()
    {
        //Arrange
        List<string> currentRuleList = [];
        Dictionary<string, List<string>> callDictionary = new() { { "null", currentRuleList } };

        EmailAddress emailAddress = new();

        Mock<IValidationBuilder> mockValidationBuilder = new();

        mockValidationBuilder
           .Setup(x => x.RequiredRule())
           .Callback(() =>
            {
                List<string> newList = [];
                currentRuleList = callDictionary.TryAdd("RequiredRule()", newList)
                    ? newList
                    : callDictionary["RequiredRule()"];
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => ReferenceEquals(obj, emailAddress.Address)),
                               It.Is<string>(s => s == nameof(EmailAddress.Address))))
           .Callback(() => currentRuleList.Add("Validate(Address)"))
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder.Setup(x => x.GetResult()).Returns(new ValidationResult());

        EmailAddressValidator sut = new(mockValidationBuilder.Object);

        //Act
        ValidationResult result = sut.Validate(emailAddress);

        //Assert
        Assert.AreEqual(callDictionary.Count(x => x.Key != "null"), 1, "Additional ValidationRules were called that should not have been");

        if (callDictionary.TryGetValue("null", out List<string>? nullList))
            Assert.IsTrue(nullList.Count == 0, "Validate call(s) made without a validator");

        if (!callDictionary.TryGetValue("RequiredRule()", out List<string>? requiredList))
            Assert.Fail("RequiredRule() not called");

        Assert.AreEqual(requiredList.Count, 1);
        Assert.IsTrue(requiredList.Contains("Validate(Address)"), "Address not validated on RequiredRule");

        mockValidationBuilder.Verify(x => x.GetResult(), Times.Once);
    }
}
