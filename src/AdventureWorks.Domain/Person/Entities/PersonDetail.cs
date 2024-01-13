namespace AdventureWorks.Domain.Person.Entities;

public class PersonDetail : Person
{
    public List<EmailAddress> EmailAddresses { get; init; } = [];

    public List<PhoneNumber> PhoneNumbers { get; init; } = [];

    public List<Address> Addresses { get; init; } = [];
}