using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class PersonValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAllValidationsMet_IsValid()
    {
        var person = GetValidPerson();
        
        var sut = new PersonValidator(new ValidationBuilder());
        
        var result = sut.Validate(person);
        
        Assert.IsTrue(result.IsValidRequest);
        
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenNameIsNull_IsNotValid()
    {

        var person = GetValidPerson();
        person.Name = null;

        var sut = new PersonValidator(new ValidationBuilder());

        var result = sut.Validate(person);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenPersonTypeIsNull_IsNotValid()
    {

        var person = GetValidPerson();
        person.PersonType = null;

        var sut = new PersonValidator(new ValidationBuilder());

        var result = sut.Validate(person);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenPersonTypeIsEmpty_IsNotValid()
    {

        var person = GetValidPerson();
        person.PersonType = string.Empty;

        var sut = new PersonValidator(new ValidationBuilder());

        var result = sut.Validate(person);

        Assert.IsFalse(result.IsValidRequest);
    }

    private Person GetValidPerson()
    {
        return new Person { Name = new PersonName { FirstName = "John", LastName = "Doe", }, PersonType = "EM" };
    }
}