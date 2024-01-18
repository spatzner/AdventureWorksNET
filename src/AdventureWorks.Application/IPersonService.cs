using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Application;

public interface IPersonService
{
    Task<SearchResult<Person>> SearchAsync(PersonSearch criteria);
    Task<QueryResult<PersonDetail>> GetAsync(int id);
    Task<OperationResult> AddAsync(PersonDetail person);
}