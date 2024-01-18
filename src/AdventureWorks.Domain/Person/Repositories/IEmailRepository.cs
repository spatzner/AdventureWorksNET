using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IEmailRepository
{
    Task AddAsync(int personId, EmailAddress emailAddress);
}