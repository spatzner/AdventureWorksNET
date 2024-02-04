using AdventureWorks.Common.Validation;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Person.Validation;
using Moq;

namespace Tests.Domain.Validation;

[TestClass]
public class GeoPointValidatorTests
{
    private static Dictionary<string, List<string>> _callDictionary = [];
    private static readonly Stack<string> CallStack = [];

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        //Arrange
        List<string> currentRuleList = [];
        _callDictionary = new Dictionary<string, List<string>> { { "null", currentRuleList } };

        GeoPoint geoPoint = new(0, 0);

        Mock<IValidationBuilder> mockValidationBuilder = new();

        mockValidationBuilder
           .Setup(x => x.RangeRule(-90, 90, true, true))
           .Callback(() =>
            {
                CallStack.Push("RangeRule(-90, 90)");
                List<string> newList = [];
                currentRuleList = _callDictionary.TryAdd("RangeRule(-90, 90)", newList)
                    ? newList
                    : _callDictionary["RangeRule(-90, 90)"];
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.RangeRule(-180, 180, true, true))
           .Callback(() =>
            {
                CallStack.Push("RangeRule(-180, 180)");
                List<string> newList = [];
                currentRuleList = _callDictionary.TryAdd("RangeRule(-180, 180)", newList)
                    ? newList
                    : _callDictionary["RangeRule(-180, 180)"];
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => obj is decimal && (decimal)obj == geoPoint.Latitude),
                It.Is<string>(s => s == nameof(GeoPoint.Latitude))))
           .Callback(() =>
            {
                CallStack.Push("Validate(Latitude)");
                currentRuleList.Add("Validate(Latitude)");
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.Validate(It.Is<object?>(obj => obj is decimal && (decimal)obj == geoPoint.Longitude),
                It.Is<string>(s => s == nameof(GeoPoint.Longitude))))
           .Callback(() =>
            {
                CallStack.Push("Validate(Longitude)");
                currentRuleList.Add("Validate(Longitude)");
            })
           .Returns(mockValidationBuilder.Object);

        mockValidationBuilder
           .Setup(x => x.GetResult())
           .Callback(() => CallStack.Push("GetResult()"))
           .Returns(new ValidationResult());

        GeoPointValidator sut = new(mockValidationBuilder.Object);

        //Act
        ValidationResult result = sut.Validate(geoPoint);
    }

    [TestMethod]
    public void Validate_WhenCalled_DoesNotCallAdditionalValidationRules()
    {
        Assert.AreEqual(_callDictionary.Count(x => x.Key != "null"), 2);
    }

    [TestMethod]
    public void Validate_WhenCalled_DoesNotValidatorWithoutARuleSet()
    {
        if (_callDictionary.TryGetValue("null", out List<string>? nullList))
            Assert.IsTrue(nullList.Count == 0);
    }

    [TestMethod]
    public void Validate_WhenCalled_ValidatesCorrectFieldsForRangRule_N180_180()
    {
        if (!_callDictionary.TryGetValue("RangeRule(-180, 180)", out List<string>? rangeRuleN180180List))
            Assert.Fail("RangeRule(-180, 180) not called");
        else
        {
            Assert.IsTrue(rangeRuleN180180List.Contains("Validate(Longitude)"));
            Assert.AreEqual(1, rangeRuleN180180List.Count);
        }
    }

    [TestMethod]
    public void Validate_WhenCalled_ValidatesCorrectFieldsForRangRule_N190_90()
    {
        if (!_callDictionary.TryGetValue("RangeRule(-90, 90)", out List<string>? rangeRuleN9090))
            Assert.Fail("RangeRule(-90, 90) not called");
        else
        {
            Assert.IsTrue(rangeRuleN9090.Contains("Validate(Latitude)"));
            Assert.AreEqual(1, rangeRuleN9090.Count);
        }
    }

    [TestMethod]
    public void Validate_WhenCalled_GetResultIsLastAction()
    {
        Assert.AreEqual("GetResult()", CallStack.Pop());
    }
}