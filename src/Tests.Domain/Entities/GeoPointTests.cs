using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;
using Tests.Shared;

namespace Tests.Domain.Entities;

[TestClass]
public class GeoPointTests
{
    [TestMethod]
    [TestCategory(TestType.Unit)]
    public void GeoPoint_WhenComparedToSameValue_AreEqual()
    {
        GeoPoint sut = new(1, 2);
        GeoPoint other = new(1, 2);

        Assert.IsTrue(sut.Equals((object)other));
        Assert.IsFalse(sut != other);
    }
}
