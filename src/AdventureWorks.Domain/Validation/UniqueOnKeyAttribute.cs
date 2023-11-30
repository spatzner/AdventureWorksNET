using System.Collections;
using System.Reflection;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class UniqueOnKeyAttribute: PropertyValidationAttribute
    {
        private List<PropertyInfo> _keyProperties = null!;
        private Type _itemType = null!;
        public override bool IsValid(string propertyName, object? value)
        {
            if (value == null)
                return true;

            if (value is not IList list)
                throw new ArgumentException($"Property must implement List<>", nameof(value));

            if(list.Count < 2)
                return true;

            _itemType = list.GetType().GetGenericArguments()[0];

            _keyProperties = _itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.GetCustomAttributes(typeof(ValidationKeyAttribute)).Any()).ToList();

            if(!_keyProperties.Any())
                throw new ArgumentException($"{_itemType.Name} must have at least one property with {nameof(ValidationKeyAttribute)}", nameof(list));

            List< Dictionary<string, object?>> keyValues = new();

            foreach (var item in list)
            {
                Dictionary<string, object?> keys = new();

                foreach (PropertyInfo keyProperty in _keyProperties)
                {
                    keys[keyProperty.Name] = keyProperty.GetValue(item);
                }

                keyValues.Add(keys);
            }

            for (int i = 0; i < keyValues.Count - 1; i++)
            for (int j = i + 1; j < keyValues.Count; j++)
            {
                if (!keyValues[i].Except(keyValues[j]).Any())
                    return false;
            }

            return true;
        }

        public override ValidationError GetErrorMessage(string propertyName, object? value)
        {

            return new ValidationError
            {
                Field = propertyName,
                Value = value,
                Type = ValidationType.UniqueList,
                Requirements =
                    $"Key values must be unique. Type: {_itemType.FullName} KeyProperties: {string.Join(", ", _keyProperties.Select(prop => prop.Name))}"
            };
        }
    }
}