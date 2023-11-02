namespace AdventureWorks.Domain.Entities;

public struct Address
{
    public Address(string address1, string address2, string city, string state, string country, string zipCode,
        GeoPoint? geoLocation)
    {
        Address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
        Address2 = address2;
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        Country = country ?? throw new ArgumentNullException(nameof(country));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        GeoLocation = geoLocation;
    }

    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public GeoPoint? GeoLocation { get; set; }
}