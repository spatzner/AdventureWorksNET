using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.SqlRepository;

public class UnitOfWorkProvider(IServiceProvider services) : IUnitOfWorkProvider
{
    public IUnitOfWork Create()
    {
        return (IUnitOfWork)services.GetRequiredService(typeof(IUnitOfWork));
    }
}