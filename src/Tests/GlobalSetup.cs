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
            ServiceProvider.InitializeProvider();




        }
    }
}
