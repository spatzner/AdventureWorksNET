using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class RangeRule : IValidationRule
    {
        public double Min { get; }
        public double Max { get; }
        public bool MinInclusive { get; }
        public bool MaxInclusive { get; }

        public RangeRule(double min, double max, bool minInclusive = true, bool maxInclusive = true)
        {
            Min = min;
            Max = max;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
        }
        
        public bool Validate(string propertyName, object? obj, out ValidationError? result)
        {
            if (obj == null)
            {
                result = GetErrorMessage(propertyName, obj);
                return false;
            }

            var dbl = Convert.ToDouble(obj);

            switch (MinInclusive)
            {
                case true when dbl < Min:
                case false when dbl <= Min:
                    result = GetErrorMessage(propertyName, obj);
                    return false;
            }

            switch (MaxInclusive)
            {
                case true when dbl > Max:
                case false when dbl >= Max:
                    result = GetErrorMessage(propertyName, obj);
                    return false;
            }

            result = null;
            return true;
        }

        public  ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                ValidationType = ValidationType.MinLength,
                Requirements = $"{Min} {(MinInclusive? "<=" : "<")} value {(MaxInclusive ? "<=" : "<")} {Max}"
            };
        }
    }
}
