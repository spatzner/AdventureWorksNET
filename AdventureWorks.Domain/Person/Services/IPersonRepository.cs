namespace AdventureWorks.Domain.Person.Services;

public interface IPersonRepository
{
    public Task<PersonDetail> GetPerson(int id);
    public Task<List<Person>> Search(PersonSearch criteria);
    public Task AddPerson(Person person);
    public Task UpdatePerson(Person person);
    public Task DeletePerson(int id);
}