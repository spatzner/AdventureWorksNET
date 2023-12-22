using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPersonRepository
{
    public Task<QueryResult<PersonDetail>> GetPerson(int id);
    public Task<SearchResult<Entities.Person>> SearchPersons(PersonSearch criteria, int maxResults);
    public Task<AddResult> AddPerson(Entities.Person person);
    public Task<int> UpdatePerson(Entities.Person person);
}