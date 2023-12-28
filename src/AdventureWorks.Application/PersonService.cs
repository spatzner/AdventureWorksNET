using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using AdventureWorks.Domain.Person.Validation;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Application
{
    public class PersonService(
            IPersonRepository repository,
            IValidator<PersonDetail> personDetailValidator,
            IValidator<PersonSearch> personSearchValidator)
        : IPersonService
    {
        public async Task<SearchResult<Person>> Search(PersonSearch criteria)
        {
            var validationResult = personSearchValidator.Validate(criteria);

            if (!validationResult.IsValidRequest)
                return new SearchResult<Person>(validationResult);

            return await repository.SearchPersons(criteria, 100);
        }

        public async Task<QueryResult<PersonDetail>> Get(int id)
        {
            return await repository.GetPerson(id);
        }

        public async Task<OperationResult> Add(PersonDetail person)
        {
            var validationResult = personDetailValidator.Validate(person);
            if (!validationResult.IsValidRequest)
                return new OperationResult(validationResult);

            return await repository.AddPerson(person);
        }
    }
}