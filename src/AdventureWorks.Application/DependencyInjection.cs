using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Repositories;
using AdventureWorks.Domain.Person.Validation;
using AdventureWorks.SqlRepository;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PersonDetail>, PersonDetailValidator>();
            services.AddScoped<IValidator<PersonSearch>, PersonSearchValidator>();
        }
    }
}
