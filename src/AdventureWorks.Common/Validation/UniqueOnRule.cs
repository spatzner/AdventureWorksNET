﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace AdventureWorks.Common.Validation;

internal class UniqueOnRule<T> : ValidationRule
{
    private readonly IEnumerable<PropertyInfo> _keyProperties;

    internal UniqueOnRule(Expression<Func<T, object?>> keys)
    {
        if (keys.Body is not MemberExpression
         && keys.Body is not NewExpression
         && keys.Body is not UnaryExpression { Operand: MemberExpression })
            throw new ArgumentException("Must provide a member expression", nameof(keys));

        List<string> members = [];

        switch (keys.Body)
        {
            case UnaryExpression unary:
                if (unary.Operand is MemberExpression expression)
                    members.Add(expression.Member.Name);
                break;
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
            result = null; //even though it doesn't meet requirement, RequiredRule is meant to catch nulls
            return true;
        }

        if (value is not IEnumerable<T> list)
            throw new ArgumentException($"Must be IEnumerable of type {typeof(T).FullName}");

        HashSet<int> hashes = [];
        int listLength = 0;

        foreach (T item in list)
        {
            listLength++;

            HashCode hashCode = new();

            foreach (PropertyInfo keyProperty in _keyProperties)
                hashCode.Add(keyProperty.GetValue(item));

            hashes.Add(hashCode.ToHashCode());

            if (listLength == hashes.Count)
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
            Requirements = string.Join(", ", _keyProperties.Select(x => x.Name))
        };
    }
}