using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation
{
    public class GeoPointValidator : IValidator<GeoPoint>
    {
        public ValidationResult Validate(GeoPoint entity)
        {
            ValidationResult result = new();

            if (!new RangeRule(-90, 90).Validate(nameof(entity.Latitude), entity.Latitude, out ValidationError? result1))
                result.Errors.Add(result1!);

            if (!new RangeRule(-180, 180).Validate(nameof(entity.Longitude), entity.Longitude, out ValidationError? result2))
                result.Errors.Add(result2!);

            return result;
        }
    }
}