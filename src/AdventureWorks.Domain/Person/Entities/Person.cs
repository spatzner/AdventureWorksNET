using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities;

public class Person
{
    public int? Id { get; set; }
    public PersonName? Name { get; set; }
    public string? PersonType { get; set; }
    public DateTime? LastModified { get; set; }

}