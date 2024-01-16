using AdventureWorks.Common.Validation;

namespace AdventureWorks.Domain.Person.Entities;

public class EmailAddress : IValidatable
{
    public int? Id { get; set; }

    public virtual string? Address { get; set; }
}