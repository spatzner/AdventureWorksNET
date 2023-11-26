using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Repositories;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using AdventureWorks.Domain;
using AdventureWorks.SqlRepository;

namespace Microsoft.Extensions.DependencyInjection
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
