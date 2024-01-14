using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public class GeoPointValidator(IRuleProvider ruleProvider) : Validator<GeoPoint>(ruleProvider)
{
    public override ValidationResult Validate(GeoPoint entity)
    {
        ValidationResult result = new();

        if (RuleProvider.RangeRule(-90, 90)
           .IsInvalid(nameof(entity.Latitude), entity.Latitude, out ValidationError? result1))
            result.Errors.Add(result1);

        if (RuleProvider.RangeRule(-180, 180)
           .IsInvalid(nameof(entity.Longitude), entity.Longitude, out ValidationError? result2))
            result.Errors.Add(result2);

        return result;
    }
}