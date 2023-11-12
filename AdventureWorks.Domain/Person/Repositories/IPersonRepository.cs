using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPersonRepository
{
    public Task<PersonDetail> Get(int id);
    public Task<List<Entities.Person>> Search(DTOs.PersonSearch criteria);
    public Task AddPerson(Entities.Person person);
    public Task UpdatePerson(Entities.Person person);
    public Task DeletePerson(int id);
}