namespace AdventureWorks.Domain.Person;

public readonly struct PersonName
{
    public PersonName(string? prefix, string firstName, string? middleName, string lastName, string? suffix)
    {
        Prefix = prefix;
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        MiddleName = middleName;
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Suffix = suffix;
    }

    public string? Prefix { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string? Suffix { get; }
}