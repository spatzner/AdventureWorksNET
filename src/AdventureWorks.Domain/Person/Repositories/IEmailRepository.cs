using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IEmailRepository
{
    Task Add(int personId, EmailAddress emailAddress);
}