﻿namespace AdventureWorks.Domain.Person;

public class Person
{
    public int? Id { get; set; }
    public required PersonName Name { get; set; }
    public required string PersonType { get; set; }
    public DateTime LastModified { get; set; }

}