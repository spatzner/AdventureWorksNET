namespace AdventureWorks.Domain.Person;

public enum ValidationType
{
    MinLength,
    MaxLength,
    UniqueList,
    IsNotEmpty,
    MinValue,
    Required,
    DiscreetValue,
    Range
}