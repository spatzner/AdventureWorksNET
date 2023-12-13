using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Application
{
    public interface IPersonService
    {
        Task<SearchResult<Domain.Person.Entities.Person>> Search(PersonSearch criteria);
        Task<QueryResult<PersonDetail>> Get(int id);
        Task<OperationResult> Add(PersonDetail person);
    }
}