using AdventureWorks.Domain.Person;
using AdventureWorks.SqlRepository;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            PersonRepository repo=
                new(Settings.ConnectionString);

            var person = await repo.GetPerson(6);
        }

        [TestMethod]
        public void TestAddress()
        {
            var x = new PhoneNumber("(697) 555 0142", "Cell");


            var y = new PhoneNumber("1 (11) 500 555-0174", "Cell");


            var s = new PhoneNumber("1 (11) 500 5555-0174", "Cell");
        }
    }
}