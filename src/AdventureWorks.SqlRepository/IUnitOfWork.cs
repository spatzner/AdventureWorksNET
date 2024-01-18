using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.SqlRepository;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAddressRepository AddressRepository { get; }
    IEmailRepository EmailRepository { get; }
    IPersonRepository PersonRepository { get; }
    IPhoneRepository PhoneRepository { get; }
    Task BeginTransactionAsync();
    Task SaveAsync();
    Task RollbackAsync();
}