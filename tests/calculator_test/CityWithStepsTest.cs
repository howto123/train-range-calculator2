using Microsoft.VisualStudio.TestTools.UnitTesting;
using calculator.CityTypes;
using calculator.JsonInterface;

namespace calculator_test;

[TestClass]
public class CityWithStepsTest
{
    private readonly string pathSetA = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range-calculator\test\testSetA.json";
    
    [TestMethod]
    public void CreateFromList_CreatesCorrectList()
    {
        var stringList = JsonReaderWriter.ReadListFromJSON(pathSetA);
        var directList = CityDirect.GetManyFromStringList(stringList);
        var finalList = CityWithSteps.CreateListFromList(3, directList);
        finalList.ForEach( c => Console.WriteLine(c));

    }
}