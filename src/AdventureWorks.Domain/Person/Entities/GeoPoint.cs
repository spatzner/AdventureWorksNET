using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities;

public class GeoPoint : IEquatable<GeoPoint>, IValidatable
{
    public double Latitude { get; }
    public double Longitude { get; }

    public GeoPoint(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public bool Equals(GeoPoint? other)
    {
        return other is not null && Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
    }

    public override bool Equals(object? obj)
    {
        return obj is GeoPoint other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }

    public static bool operator ==(GeoPoint left, GeoPoint? right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GeoPoint left, GeoPoint? right)
    {
        return !(left == right);
    }
}