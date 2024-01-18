using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories;

public interface IPersonRepository
{
    public Task<QueryResult<PersonDetail>> GetPersonAsync(int id);
    public Task<SearchResult<Entities.Person>> SearchPersonsAsync(PersonSearch criteria, int maxResults);
    public Task<AddResult> AddAsync(Entities.Person person);
    public Task<int> UpdateAsync(Entities.Person person);
}