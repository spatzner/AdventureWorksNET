using AdventureWorks.Domain.Person.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.SqlRepository
{
    public static class DependencyInjection
    {
        public static void AddRepositoryServices(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            services.AddSingleton(_ => connectionStrings);
            services.AddSingleton<IConnectionProvider, ConnectionProvider>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
