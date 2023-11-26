using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.SqlRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    internal class ServiceProvider
    {
        private static IServiceProvider? _instance;

        public static T GetService<T>()
        {
            return _instance!.GetService<T>() ?? throw new ArgumentException($"{typeof(T)} is not configured");
        }


        public static void InitializeProvider()
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Provider already initialized");
            }

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            ConnectionStrings connectionStrings = new();

            config.GetSection("ConnectionStrings").Bind(connectionStrings);

            var services = new ServiceCollection();

            services.AddRepositoryServices(connectionStrings);
            services.AddDomainServices();

            _instance = services.BuildServiceProvider();
        }
    }
}
