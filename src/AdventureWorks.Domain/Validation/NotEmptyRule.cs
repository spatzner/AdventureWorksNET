using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    internal class NotEmptyRule : IValidationRule
    {


        public bool Validate(string propertyName, object? value, out ValidationError? result)
        {
            bool isValid;
            switch (value)
            {
                case null:
                    isValid = false;
                    break;
                case string s:
                    isValid = !string.IsNullOrEmpty(s);
                    break;
                case IEnumerable<object> ie:
                    isValid = ie.Any();
                    break;
                default:
                    isValid = value.GetType()
                        .GetProperties(BindingFlags.Public)
                        .Any(prop => prop.GetValue(value) != default);
                    break;
            }

            result = isValid ? null : GetErrorMessage(propertyName, value);
            return isValid;
        }

        public ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                ValidationType = ValidationType.IsNotEmpty,
                Requirements = string.Empty
            };
        }
    }
}
