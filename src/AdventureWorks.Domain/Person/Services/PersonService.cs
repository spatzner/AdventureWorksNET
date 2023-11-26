using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.Domain.Person.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IEmailRepository _emailRepository;

        public PersonService(IPersonRepository personRepository, IAddressRepository addressRepository, IPhoneRepository phoneRepository, IEmailRepository emailRepository)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _phoneRepository = phoneRepository;
            _emailRepository = emailRepository;
        }

        public async Task<SearchResult<Entities.Person>> SearchPerson(PersonSearch criteria)
        {
            //TODO: validate input

            return await _personRepository.SearchPersons(criteria, 100);
        }

        public async Task<PersonDetail> GetPerson(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"{nameof(id)} must be a positive integer");

            return await _personRepository.GetPerson(id);
        }

        public async Task<int> AddPerson(PersonDetail person)
        {
            //TODO: validate input

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            int id = await _personRepository.AddPerson(person);

            foreach (var address in person.Addresses)
                await _addressRepository.Add(id, address);
            
            foreach (var phoneNumber in person.PhoneNumbers)
                await _phoneRepository.Add(id, phoneNumber);

            foreach (var email in person.EmailAddresses)
                await _emailRepository.Add(id, email);
            

            scope.Complete();

            return id;
        }
    }
}
