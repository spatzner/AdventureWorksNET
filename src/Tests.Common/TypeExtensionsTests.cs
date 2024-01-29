using AdventureWorks.Common;

namespace Tests.Common;

[TestClass]
public class TypeExtensionsTests
{
    [TestMethod]
    public void IsIntegralValueType_WhenIntegralValueTypes_ReturnsTrue()
    {
        Assert.IsTrue(((object)(sbyte)1).IsIntegralValueType());
        Assert.IsTrue(((object)(byte)1).IsIntegralValueType());
        Assert.IsTrue(((object)(short)1).IsIntegralValueType());
        Assert.IsTrue(((object)(ushort)1).IsIntegralValueType());
        Assert.IsTrue(((object)1).IsIntegralValueType());
        Assert.IsTrue(((object)(uint)1).IsIntegralValueType());
        Assert.IsTrue(((object)(long)1).IsIntegralValueType());
        Assert.IsTrue(((object)(ulong)1).IsIntegralValueType());
        Assert.IsTrue(((object)(nint)1).IsIntegralValueType());
        Assert.IsTrue(((object)(nuint)1).IsIntegralValueType());
    }

    [TestMethod]
    public void IsIntegralValueType_WhenNonIntegralValueTypes_ReturnsFalse()
    {
        Assert.IsFalse(((object)1.0).IsIntegralValueType());
        Assert.IsFalse(((object)"test").IsIntegralValueType());
        Assert.IsFalse(((object)new object()).IsIntegralValueType());
    }
}