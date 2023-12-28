using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class UniqueRule<T> : ValidationRule
    {
        private readonly IEnumerable<PropertyInfo> _keyProperties;

        public UniqueRule(Expression<Func<T, object?>> keys)
        {
            if (keys.Body.Type == typeof(MemberExpression) || keys.Body.Type == typeof(NewExpression))
                throw new ArgumentException($"Must provide a member expression", nameof(keys));

            List<string> members = [];

            switch (keys.Body)
            {
                case MemberExpression member:

                    members.Add(member.Member.Name);
                    break;

                case NewExpression newExpression:

                    if (newExpression.Members != null)
                        members.AddRange(newExpression.Members.Select(memberInfo => memberInfo.Name));
                    break;
            }

            if (members.Count == 0)
                throw new ArgumentException("Must provide at least one property");

            _keyProperties = typeof(T).GetProperties().Where(x => members.Contains(x.Name));
        }

        public override bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result)
        {
            if (value == null)
            {
                result = null;
                return true;
            }

            if (value is not IEnumerable<T> list)
            {
                result = GetErrorMessage(propertyName, value);
                return false;
            }

            List<Dictionary<string, object?>> keyValues = [];

            foreach (T item in list)
            {
                Dictionary<string, object?> keys = [];

                foreach (PropertyInfo keyProperty in _keyProperties)
                {
                    keys[keyProperty.Name] = keyProperty.GetValue(item);
                }

                keyValues.Add(keys);
            }

            for (int i = 0; i < keyValues.Count - 1; i++)
            for (int j = i + 1; j < keyValues.Count; j++)
            {
                if (keyValues[i].Except(keyValues[j]).Any())
                    continue;
                
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
                ValidationType = ValidationType.UniqueList,
                Requirements = $"Values must be unique on {string.Join(", ", _keyProperties.Select(x => x.Name))}"
            };
        }
    }
}
