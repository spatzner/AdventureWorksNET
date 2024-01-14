using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class GeoPointValidator(IValidationBuilder validationBuilder) : Validator<GeoPoint>(validationBuilder)
{
    public override ValidationResult Validate(GeoPoint entity)
    {
        ValidationBuilder.RangeRule(-90, 90).Validate(nameof(entity.Latitude), entity.Latitude);
        ValidationBuilder.RangeRule(-180, 180).Validate(nameof(entity.Longitude), entity.Longitude);

        return ValidationBuilder.GetResult();
    }
}