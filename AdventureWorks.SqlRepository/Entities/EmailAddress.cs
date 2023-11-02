namespace AdventureWorks.SqlRepository.Entities;

public class EmailAddress
{
    public int BusinessEntityId { get; set; }
    public int EmailAddressId { get; set; }
    public string? Address { get; set; }
}