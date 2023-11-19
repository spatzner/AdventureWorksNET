using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Tests
{
    [TestClass]
    public class GlobalSetup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            AppSettings settings = new();

            config.GetSection("TestSettings").Bind(settings);

            Settings.SetInstance(settings);

            
        }
    }
}
