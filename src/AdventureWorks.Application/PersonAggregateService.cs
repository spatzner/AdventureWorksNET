using System.Transactions;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.Application
{
    public class PersonAggregateService(
        IPersonRepository personRepository,
        IAddressRepository addressRepository,
        IPhoneRepository phoneRepository,
        IEmailRepository emailRepository,
        IValidationService validationService)
    {
        public async Task<SearchResult<Person>> Search(PersonSearch criteria)
        {
            var validationResult = validationService.Validate(criteria);
            if (!validationResult.IsValidRequest)
                return new SearchResult<Person>(validationResult);

            return await personRepository.SearchPersons(criteria, 100);
        }

        public async Task<QueryResult<PersonDetail>> Get(int id)
        {
            return await personRepository.GetPerson(id);
        }

        public async Task<AddResult> Add(PersonDetail person)
        {
            validationService.Validate(person);

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            AddResult personResult = await personRepository.AddPerson(person);

            if (!personResult.Success)
                return personResult;

            int id = personResult.Id;

            foreach (var address in person.Addresses)
                await addressRepository.Add(id, address);

            foreach (var phoneNumber in person.PhoneNumbers)
                await phoneRepository.Add(id, phoneNumber);

            foreach (var email in person.EmailAddresses)
                await emailRepository.Add(id, email);

            scope.Complete();

            return personResult;
        }
    }
}