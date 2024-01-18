using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPhoneRepository
{
    Task AddAsync(int personId, PhoneNumber phoneNumber);
}