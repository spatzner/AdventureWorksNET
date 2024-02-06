using AdventureWorks.Common.Validation;

namespace AdventureWorks.Domain.Person.Entities;

public readonly struct GeoPoint(decimal latitude, decimal longitude): IEquatable<GeoPoint>, IValidatable
{
    public decimal Latitude { get; } = latitude;
    public decimal Longitude { get; } = longitude;

    public bool Equals(GeoPoint other)
    {
        return Latitude == other.Latitude && Longitude == other.Longitude;
    }

    public override bool Equals(object? obj)
    {
        return obj is GeoPoint other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }

    public static bool operator ==(GeoPoint left, GeoPoint right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GeoPoint left, GeoPoint right)
    {
        return !(left == right);
    }
}