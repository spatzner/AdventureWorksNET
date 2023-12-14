using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class RangeRule : ValidationRule
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
        
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            if (value == null)
            {
                result = GetErrorMessage(propertyName, value);
                return false;
            }

            var dbl = Convert.ToDouble(value);

            switch (MinInclusive)
            {
                case true when dbl < Min:
                case false when dbl <= Min:
                    result = GetErrorMessage(propertyName, value);
                    return false;
            }

            switch (MaxInclusive)
            {
                case true when dbl > Max:
                case false when dbl >= Max:
                    result = GetErrorMessage(propertyName, value);
                    return false;
            }

            result = null;
            return true;
        }

        protected override ValidationError GetErrorMessage(string propertyName, object? value)
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
