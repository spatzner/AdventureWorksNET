namespace AdventureWorks.Domain.Person.Entities;

public class Address : IValidatable
{
    public virtual int? Id { get; set; }
    public virtual string? Type { get; set; }
    public virtual string? Address1 { get; set; }
    public virtual string? Address2 { get; set; }
    public virtual string? City { get; set; }
    public virtual string? State { get; set; }
    public virtual string? Country { get; set; }
    public virtual string? PostalCode { get; set; }
    public virtual GeoPoint? GeoLocation { get; set; }
}