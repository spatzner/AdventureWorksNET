namespace AdventureWorks.Domain.Person.Entities;

public class PersonName
{
    public string? Title { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public string? Suffix { get; set; }
}