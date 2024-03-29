﻿namespace AdventureWorks.Common.Validation;

public class ValidationError
{
    public ValidationType ValidationType { get; set; }
    public required string Field { get; set; }
    public object? Value { get; set; }
    public required string Requirements { get; set; }
    private Stack<string> PropertyStack { get; } = new();
    public string PropertyHierarchy => string.Join(".", PropertyStack);

    public void AddToPropertyHierarchy(string propertyName)
    {
        PropertyStack.Push(propertyName);
    }
}