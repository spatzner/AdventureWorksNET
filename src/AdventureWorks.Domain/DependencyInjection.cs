using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Domain
{
    public static class DependencyInjection
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
