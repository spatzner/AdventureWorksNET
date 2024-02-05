using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Common.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Common;
public static class DependencyInjection
{
    public static void AddCommonServices(this IServiceCollection services)
    {

        services.AddScoped<IValidationBuilder, ValidationBuilder>();
    }
}
