namespace AdventureWorks.SqlRepository;

public interface IUnitOfWorkProvider
{
    IUnitOfWork Create();
}