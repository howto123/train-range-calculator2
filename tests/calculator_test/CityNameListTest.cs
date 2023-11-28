using calculator.CityTypes;
using calculator.JsonInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace calculator_test;

[TestClass]
public class CityNameListTest
// TODO: now, this is dependent on input files as well as JsonReaderWriter.
// An interface and different sources of the private attributes configurable by a setting
// or a concrete implementation of the interface would be better

{
    
    private readonly string pathSetA = $@"{System.IO.Directory.GetCurrentDirectory()}\testSetA.json";
    private readonly string pathCityX = $@"{System.IO.Directory.GetCurrentDirectory()}\testCityX.json";
    //private readonly string pathSetB = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range\train-range-calculator2\test\testSetB.json";

    private readonly List<CityNameList> citiesAsStringSet;
    private readonly CityNameList cityNameList;
    private readonly List<CityBasic> citySet;

    public CityNameListTest()
    {
        citiesAsStringSet  = JsonReaderWriter.ReadListFromJSON(pathSetA);
        cityNameList  = JsonReaderWriter.ReadOneFromJSON(pathCityX);
        citySet  = new List<CityBasic>(citiesAsStringSet);
        
    }

    [TestMethod]
    public void GetFromStringSet_HasCorrectDirectlyReachable()
    {
        Debug.WriteLine($"Path is:");
        Debug.WriteLine(System.IO.Directory.GetCurrentDirectory());
        var realReachable = CityDirect.CreateValid(cityNameList, citiesAsStringSet)
            .DirectlyReachable;

        var shouldBeList = new List<CityBasic>(citySet).FindAll(c => c.Name == "Bern").OrderBy(x => x);
        
        Assert.IsTrue(realReachable.OrderBy(x => x).SequenceEqual(shouldBeList));
    }

    [TestMethod]
    public void GetSetFromStringSet_ThunAndMeiringenShouldBeThere_And_HaveCorrectICityValues()
    {
        var iCityDirectSet = CityDirect.GetManyFromStringList(citiesAsStringSet);

        // is
        var cityDirectList = new List<CityDirect>(iCityDirectSet);
        var mayBeThun = cityDirectList.Find( c => c.Name == "Thun" );
        var mayBeMeiringen = cityDirectList.Find( c => c.Name == "Thun" );

        var shouldBeList = new List<CityBasic>(citySet);
        var shoulBeThun = shouldBeList.Find( c => c.Name == "Thun" );
        var shoulBeMeiringen = shouldBeList.Find( c => c.Name == "Thun" );


        Assert.IsNotNull(mayBeThun);
        Assert.IsNotNull(mayBeMeiringen);
        Assert.IsNotNull(shoulBeThun);
        Assert.IsNotNull(shoulBeMeiringen);
        Assert.IsTrue(CityDirect.CompareICities(mayBeThun, shoulBeThun));
        Assert.IsTrue(CityDirect.CompareICities(mayBeMeiringen, shoulBeMeiringen));
    }
}