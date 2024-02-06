using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Moq;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class EmailAddressValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAddressIsSupplied_IsValid()
    {
        var address = new EmailAddress { Address = "example@abc.com" };

        var sut = new EmailAddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsTrue(result.IsValidRequest);
    }
    
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAddressIsEmpty_IsNotValid()
    {
        var address = new EmailAddress { Address = string.Empty };

        var sut = new EmailAddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAddressIsNull_IsNotValid()
    {
        var address = new EmailAddress { Address = null };

        var sut = new EmailAddressValidator(new ValidationBuilder());

        var result = sut.Validate(address);

        Assert.IsFalse(result.IsValidRequest);
    }
}
