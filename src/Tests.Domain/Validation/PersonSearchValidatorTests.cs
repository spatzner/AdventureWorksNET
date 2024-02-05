using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Validation;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class PersonSearchValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenNoParametersProvided_IsInvalid()
    {
        var personSearch = new PersonSearch();
        
        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);
        
        Assert.IsFalse(result.IsValidRequest);
    }
    
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenEmailAddressProvided_IsValid()
    {

        var personSearch = new PersonSearch
        {
            EmailAddress = "email@test.com"
        };

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenMiddleNameProvided_IsValid()
    {

        var personSearch = new PersonSearch
        {
            MiddleName = "A"
        };

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenPersonTypeProvided_IsValid()
    {

        var personSearch = new PersonSearch
        {
            PersonType = "EM"
        };

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenPhoneNumberProvided_IsValid()
    {

        var personSearch = new PersonSearch { PhoneNumber = "123-456-7890" };

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenFirstNameProvided_IsValid()
    {

        var personSearch = new PersonSearch {FirstName = "John"};

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void PersonSearchValidator_WhenLastNameProvided_IsValid()
    {

        var personSearch = new PersonSearch { LastName = "Doe" };

        var sut = new PersonSearchValidator(new ValidationBuilder());

        ValidationResult result = sut.Validate(personSearch);

        Assert.IsTrue(result.IsValidRequest);
    }
}
