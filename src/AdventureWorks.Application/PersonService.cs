using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using AdventureWorks.Domain.Person.Validation;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Application
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IValidator<PersonDetail> _personDetailValidator;
        private readonly IValidator<PersonSearch> _personSearchValidator;

        public PersonService(IPersonRepository repository, IValidator<PersonDetail> personDetailValidator, IValidator<PersonSearch> personSearchValidator)
        {
            _repository = repository;
            _personDetailValidator = personDetailValidator;
            _personSearchValidator = personSearchValidator;
        }


        public Task<SearchResult<Person>> Search(PersonSearch criteria)
        {
            _personSearchValidator.Validate(criteria);
        }

        public Task<QueryResult<PersonDetail>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Add(PersonDetail person)
        {
            throw new NotImplementedException();
        }
    }
}