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
    public class PersonService
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
            return  await _personRepository.SearchPersons(criteria, 100);
        }

        public async Task<PersonDetail> GetPerson(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"{nameof(id)} must be a positive integer");

            return await _personRepository.GetPerson(id);
        }

        public async Task AddPerson(PersonDetail person)
        {
            //TODO: validate input

            using TransactionScope scope = new();

            int id = await _personRepository.AddPerson(person);

            List<Task> tasks = new();

            tasks.AddRange(person.Addresses.Select(address => _addressRepository.Add(id, address)));
            tasks.AddRange(person.PhoneNumbers.Select(phoneNumber => _phoneRepository.Add(id, phoneNumber)));
            tasks.AddRange(person.EmailAddresses.Select(email => _emailRepository.Add(id, email)));

            await Task.WhenAll(tasks);

            scope.Complete();
        }
    }
}
