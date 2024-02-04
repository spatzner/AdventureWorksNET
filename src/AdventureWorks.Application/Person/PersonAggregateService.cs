using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.SqlRepository;
using ValidationResult = AdventureWorks.Common.Validation.ValidationResult;

namespace AdventureWorks.Application.Person;

public class PersonAggregateService(IUnitOfWorkProvider unitOfWorkProvider, IValidationService validationService)
{
    public async Task<SearchResult<Domain.Person.Entities.Person>> SearchAsync(PersonSearch criteria)
    {
        ValidationResult validationResult = validationService.Validate(criteria);
        if (!validationResult.IsValidRequest)
            return new SearchResult<Domain.Person.Entities.Person>(validationResult);

        await using IUnitOfWork unitOfWork = unitOfWorkProvider.Create();

        return await unitOfWork.PersonRepository.SearchPersonsAsync(criteria, 100);
    }

    public async Task<QueryResult<PersonDetail>> GetAsync(int id)
    {
        await using IUnitOfWork unitOfWork = unitOfWorkProvider.Create();

        return await unitOfWork.PersonRepository.GetPersonAsync(id);
    }

    public async Task<AddResult> AddAsync(PersonDetail person)
    {
        ValidationResult validationResult = validationService.Validate(person);
        if (!validationResult.IsValidRequest)
            return new AddResult(validationResult);

        await using IUnitOfWork unitOfWork = unitOfWorkProvider.Create();

        await unitOfWork.BeginTransactionAsync();

        AddResult personResult = await unitOfWork.PersonRepository.AddAsync(person);

        if (!personResult.Success)
            return personResult;

        int id = personResult.Id;

        foreach (Address address in person.Addresses)
            await unitOfWork.AddressRepository.AddAsync(id, address);

        foreach (PhoneNumber phoneNumber in person.PhoneNumbers)
            await unitOfWork.PhoneRepository.AddAsync(id, phoneNumber);

        foreach (EmailAddress email in person.EmailAddresses)
            await unitOfWork.EmailRepository.AddAsync(id, email);

        await unitOfWork.SaveAsync();

        return personResult;
    }
}