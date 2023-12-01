using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities;

public class Address
{
    public int? Id { get; set; }

    public required string Type { get; set; }
    [MaxLength(60)]
    public required string Address1 { get; set; }
    [MaxLength(60)]
    public string? Address2 { get; set; }

    [MaxLength(30)]
    public required string City { get; set; }
    [MaxLength(50)]
    public required string State { get; set; }
    [MaxLength(50)]
    public required string Country { get; set; }
    [MaxLength(15)]
    public required string PostalCode { get; set; }
    public GeoPoint GeoLocation { get; set; }
}