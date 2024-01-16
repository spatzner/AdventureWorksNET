using AdventureWorks.Common.Validation;

namespace AdventureWorks.Domain.Person.DTOs;

public class PersonSearch : IValidatable
{
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PersonType { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNumber { get; set; }
}