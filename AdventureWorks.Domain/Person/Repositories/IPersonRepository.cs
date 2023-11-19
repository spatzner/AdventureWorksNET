using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPersonRepository
{
    public Task<PersonDetail> GetPerson(int id);
    public Task<SearchResult<Entities.Person>> SearchPersons(PersonSearch criteria, int maxResults);
    public Task<int> AddPerson(Entities.Person person);
    public Task<int> UpdatePerson(Entities.Person person);
}