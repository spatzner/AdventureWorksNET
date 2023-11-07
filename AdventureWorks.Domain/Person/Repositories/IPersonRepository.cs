namespace AdventureWorks.Domain.Person.Repositories;

public interface IPersonRepository
{
    public Task<PersonDetail> Get(int id);
    public Task<List<Person>> Search(PersonSearch criteria);
    public Task AddPerson(Person person);
    public Task UpdatePerson(Person person);
    public Task DeletePerson(int id);
}