using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Tests.Shared;

namespace Tests.Domain.Validation;

[TestClass]
public class PersonDetailValidatorTests
{
    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAllValidationsMet_IsValid()
    {
        PersonDetail personDetail = GetValidPersonDetail();

        PersonDetailValidator sut = new(new ValidationBuilder());

        ValidationResult result = sut.Validate(personDetail);

        Assert.IsTrue(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenAddressIsNotUniqueOnType_IsNotValid()
    {
        PersonDetail personDetail = GetValidPersonDetail();

        personDetail.Addresses.AddRange([
            new Address { Type = "Home" },
            new Address { Type = "Home" }
        ]);

        PersonDetailValidator sut = new(new ValidationBuilder());

        ValidationResult result = sut.Validate(personDetail);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenPhoneNumberIsNotUniqueOnType_IsNotValid()
    {
        PersonDetail personDetail = GetValidPersonDetail();

        personDetail.PhoneNumbers.AddRange([
            new PhoneNumber("555-555-4567", "Home"),
            new PhoneNumber("555-555-4567", "Home")
        ]);

        PersonDetailValidator sut = new(new ValidationBuilder());

        ValidationResult result = sut.Validate(personDetail);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenEmailAddressIsNotUniqueOnAddress_IsNotValid()
    {
        PersonDetail personDetail = GetValidPersonDetail();

        personDetail.EmailAddresses.AddRange([
            new EmailAddress { Address = "test@domain.com" },
            new EmailAddress { Address = "test@domain.com" }
        ]);

        PersonDetailValidator sut = new(new ValidationBuilder());

        ValidationResult result = sut.Validate(personDetail);

        Assert.IsFalse(result.IsValidRequest);
    }

    [TestMethod]
    [TestCategory(TestType.Component)]
    public void Validate_WhenPersonValidationFails_IsNotValid()
    {
        PersonDetail personDetail = GetValidPersonDetail();

        personDetail.Name = null;

        PersonDetailValidator sut = new(new ValidationBuilder());

        ValidationResult result = sut.Validate(personDetail);

        Assert.IsFalse(result.IsValidRequest);
    }

    private PersonDetail GetValidPersonDetail()
    {
        return new PersonDetail
        {
            Name = new PersonName { FirstName = "John", LastName = "Doe" },
            PersonType = "EM",
            Addresses = [new Address()],
            EmailAddresses = [new EmailAddress()],
            PhoneNumbers = [new PhoneNumber("555-192-8335", "Cell")]
        };
    }
}