using System.Transactions;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.Application
{
    public class PersonAggregateService(IPersonRepository personRepository,
            IAddressRepository addressRepository,
            IPhoneRepository phoneRepository,
            IEmailRepository emailRepository)
    {
        public async Task<SearchResult<Domain.Person.Entities.Person>> Search(PersonSearch criteria)
        {
            //TODO: validate input

            return await personRepository.SearchPersons(criteria, 100);
        }

        public async Task<PersonDetail> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"{nameof(id)} must be a positive integer");

            return await personRepository.GetPerson(id);
        }

        public async Task<AddResult> Add(PersonDetail person)
        {
            //TODO: validate input

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
