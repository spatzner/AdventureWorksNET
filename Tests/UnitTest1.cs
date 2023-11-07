using System.Diagnostics.CodeAnalysis;
using AdventureWorks.Domain.Person;
using AdventureWorks.SqlRepository;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAddress()
        {
#pragma warning disable IDE0059

            var x = new PhoneNumber("(697) 555 0142", "Cell");


            var y = new PhoneNumber("1 (11) 500 555-0174", "Cell");


            var s = new PhoneNumber("1 (11) 500 5555-0174", "Cell");

#pragma warning restore IDE0059
        }
    }
}