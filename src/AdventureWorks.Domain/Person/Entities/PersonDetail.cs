namespace AdventureWorks.Domain.Person.Entities;

public class PersonDetail : Person
{
    public List<EmailAddress> EmailAddresses { get; init; } = new();

    public List<PhoneNumber> PhoneNumbers { get; init; } = new();

    public List<Address> Addresses { get; init; } = new();
}