using AdventureWorks.Common.Validation;
using Tests.Shared;

namespace Tests.Common.Validation;

//rename unit test methods in this file to match the format of other unit tests in the solution
[TestClass]
public class ValidationErrorTests
{
    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void PropertyHierarchy_WhenNoPropertiesAreAdded_IsBuiltCorrectly()
    {
        ValidationError sut = new()
        {
            Field = "Field", ValidationType = ValidationType.Required, Requirements = string.Empty
        };
        
        string result = sut.PropertyHierarchy;

        Assert.AreEqual(string.Empty, result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void PropertyHierarchy_WhenOnePropertyIsAdded_IsBuiltCorrectly()
    {
        ValidationError sut = new()
        {
            Field = "Field", ValidationType = ValidationType.Required, Requirements = string.Empty
        };
        
        sut.AddToPropertyHierarchy("Property1");

        string result = sut.PropertyHierarchy;

        Assert.AreEqual("Property1", result);
    }

    [TestMethod]
    [TestCategory(Constants.Unit)]
    public void PropertyHierarchy_WhenTwoPropertiesAreAdded_IsBuiltCorrectly()
    {
        ValidationError sut = new()
        {
            Field = "Field", ValidationType = ValidationType.Required, Requirements = string.Empty
        };
        
        sut.AddToPropertyHierarchy("Property1");
        sut.AddToPropertyHierarchy("Property2");

        string result = sut.PropertyHierarchy;

        Assert.AreEqual("Property2.Property1", result);
    }
}