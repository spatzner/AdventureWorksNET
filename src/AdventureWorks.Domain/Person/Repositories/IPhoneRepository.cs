using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPhoneRepository
{
    Task Add(int personId, PhoneNumber phoneNumber);
}