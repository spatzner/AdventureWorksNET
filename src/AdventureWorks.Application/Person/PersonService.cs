using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.Application.Person;

public class PersonService(
    IPersonRepository repository,
    IValidator<PersonDetail> personDetailValidator,
    IValidator<PersonSearch> personSearchValidator) : IPersonService
{
    public async Task<SearchResult<Domain.Person.Entities.Person>> SearchAsync(PersonSearch criteria)
    {
        ValidationResult validationResult = personSearchValidator.Validate(criteria);

        if (!validationResult.IsValidRequest)
            return new SearchResult<Domain.Person.Entities.Person>(validationResult);

        return await repository.SearchPersonsAsync(criteria, 100);
    }

    public async Task<QueryResult<PersonDetail>> GetAsync(int id)
    {
        return await repository.GetPersonAsync(id);
    }

    public async Task<OperationResult> AddAsync(PersonDetail person)
    {
        ValidationResult validationResult = personDetailValidator.Validate(person);
        if (!validationResult.IsValidRequest)
            return new OperationResult(validationResult);

        return await repository.AddAsync(person);
    }
}