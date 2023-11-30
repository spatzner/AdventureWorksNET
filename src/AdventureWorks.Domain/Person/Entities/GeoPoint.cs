namespace AdventureWorks.Domain.Person.Entities;

public readonly struct GeoPoint
{
    public GeoPoint(double latitude, double longitude)
    {
        if (latitude is < -90 or > 90)
            throw new ArgumentException("Must be [-90,90]", nameof(latitude));
        if (longitude is < -180 or > 180)
            throw new ArgumentException("Must be [-180 ,180]", nameof(longitude));

        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }
    public double Longitude { get; }
}