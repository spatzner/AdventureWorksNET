using AdventureWorks.Domain.Person.Repositories;

namespace AdventureWorks.SqlRepository;

internal class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseContext _context;

    internal UnitOfWork(IDatabaseContext context,
        IAddressRepository addressRepository,
        IEmailRepository emailRepository,
        IPersonRepository personRepository,
        IPhoneRepository phoneRepository)
    {
        _context = context;
        AddressRepository = addressRepository;
        EmailRepository = emailRepository;
        PersonRepository = personRepository;
        PhoneRepository = phoneRepository;
    }

    public IAddressRepository AddressRepository { get; }
    public IEmailRepository EmailRepository { get; }
    public IPersonRepository PersonRepository { get; }
    public IPhoneRepository PhoneRepository { get; }

    public async Task BeginTransactionAsync()
    {
        await _context.BeginTransactionAsync();
    }

    public async Task SaveAsync()
    {
        await _context.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.RollbackAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _context.DisposeAsync();
    }
}