namespace Tests.SqlRepository
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
