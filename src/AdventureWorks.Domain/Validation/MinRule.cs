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
    public class MinRule<T> : ValidationRule
    {
        public T Min { get; }
        public MinRule(T min)
        {
            Min = min;

            if(min is not decimal or int or double or float)
                throw new ArgumentException($" Type {typeof(T)} is not supported");

        }
        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            bool isValid;

            if (value is null)
            {
                result = GetErrorMessage(propertyName, value);
                return false;
            }

            if (typeof(T) != value.GetType())
                throw new ArgumentException($" {nameof(value)} Type {value.GetType()} must match generic type");

            switch (value)
            {
                case null:
                    isValid = false;
                    break;
                case int i:
                    isValid = i >= Convert.ToInt32(Min);
                    break;
                case double d: 
                    isValid = d >= Convert.ToDouble(Min); 
                    break;
                case decimal d:
                    isValid = d >= Convert.ToDecimal(Min);
                    break;
                case float d:
                    isValid = d >= Convert.ToSingle(Min);
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
                Requirements = $"value must be at least {Min}",
                Field = propertyName,
                ValidationType = ValidationType.Min,
                Value = value
            };
        }
    }
}
