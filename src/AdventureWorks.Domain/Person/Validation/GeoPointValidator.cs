﻿using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Validation;

public class GeoPointValidator(IValidationBuilder validationBuilder) : Validator<GeoPoint>(validationBuilder)
{
    public override ValidationResult Validate(GeoPoint entity)
    {
        return ValidationBuilder
           .RangeRule(-90, 90)
           .Validate(entity.Latitude, nameof(entity.Latitude))
           .RangeRule(-180, 180)
           .Validate(entity.Longitude, nameof(entity.Longitude))
           .GetResult();
    }
}