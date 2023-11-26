using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Services
{
    public interface IPersonService
    {
        Task<SearchResult<Entities.Person>> SearchPerson(PersonSearch criteria);
        Task<PersonDetail> GetPerson(int id);
        Task<int> AddPerson(PersonDetail person);
    }
}