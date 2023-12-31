using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IAddressRepository
{
    Task Add(int personId, Address address);
}