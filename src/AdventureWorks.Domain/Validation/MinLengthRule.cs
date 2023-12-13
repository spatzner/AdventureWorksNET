﻿using System.Collections;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class MinLengthRule : IValidationRule
    {
        public int MinLength { get; }

        public MinLengthRule(int minLength)
        {
            MinLength = minLength;
        }


        public bool Validate(string propertyName, object? value, out ValidationError? result)
        {
            bool isValid;

            switch (value)
            {
                case null:
                    isValid = false;
                    break;
                case string s:
                    isValid = s.Length >= MinLength;
                    break;
                case IEnumerable e:
                    int count = e.Cast<object?>().Count();
                    isValid = count >= MinLength;
                    break;
                default:
                    throw new ArgumentException(
                        $"{nameof(MinLengthRule)} is not valid for type {value.GetType()}. Attribute on property {propertyName}");
            }

            result = !isValid ? GetErrorMessage(propertyName, value) : null;
            return isValid;
        }

        public ValidationError GetErrorMessage(string propertyName, object? value)
        {
            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                ValidationType = ValidationType.MinLength,
                Requirements = $"Min Length: {MinLength}"
            };
        }
    }
}