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
    public class MinValueRule<T> : ValidationRule
    {
        public T Min { get; }
        public bool MinIncluded{ get; init; }
        public MinValueRule(T min)
        {
            Min = min;
            
            if(min is not decimal 
               and not int 
               and not double 
               and not float)
                throw new ArgumentException($" Type {typeof(T)} is not supported");
        }
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            bool isValid;

            if (value is null)
            {
                result = null;
                return true;
            }

            if (typeof(T) != value.GetType())
                throw new ArgumentException($" {nameof(value)} Type {value.GetType()} must match generic type");

            switch (value)
            {
                case int i:
                    var @int = Convert.ToInt32(Min);
                    isValid = i > @int || (MinIncluded && i == @int);
                    break;
                case decimal d:
                    var @decimal = Convert.ToDecimal(Min);
                    isValid = d > @decimal || (MinIncluded && d == @decimal);
                    break;
                default:
                    throw new ArgumentException($" Type {value.GetType()} is not supported");
            }

            result = isValid ? null : GetErrorMessage(propertyName, value);
            return isValid;
        }

        protected override ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                ValidationType = ValidationType.MinValue,
                Requirements = $"Min value: {Min}"
            };
        }
    }
}
