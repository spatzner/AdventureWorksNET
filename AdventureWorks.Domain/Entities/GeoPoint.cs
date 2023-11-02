namespace AdventureWorks.Domain.Entities;

public struct GeoPoint
{
    public GeoPoint(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Invalid Latitude", nameof(latitude));
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Invalid Longitude", nameof(longitude));

        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }
    public double Longitude { get; }
}