namespace AdventureWorks.Domain.Person.Entities;

public class Address
{
    public int? Id { get; set; }
    public required string Type { get; set; }
    public required string Address1 { get; set; }
    public string? Address2 { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string PostalCode { get; set; }
    public GeoPoint GeoLocation { get; set; }
}