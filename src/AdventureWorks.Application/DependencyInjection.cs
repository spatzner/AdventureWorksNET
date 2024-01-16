using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PersonDetail>, PersonDetailValidator>();
        services.AddScoped<IValidator<PersonSearch>, PersonSearchValidator>();
    }
}