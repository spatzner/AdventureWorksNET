using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IAddressRepository
{
    Task AddAsync(int personId, Address address);
}