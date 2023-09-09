using Microsoft.VisualStudio.TestTools.UnitTesting;
using calculator.CityTypes;
using calculator.JsonInterface;
using System.Collections.Generic;

namespace calculator_test;

[TestClass]
public class CityDirectTest
// TODO: now, this is dependent on input files as well as JsonReaderWriter.
// An interface and different sources of the private attributes configurable by a setting
// or a concrete implementation of the interface would be better

{
    private readonly string pathSetA = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range-calculator\test\testSetA.json";
    private readonly string pathCityX = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range-calculator\test\testCityX.json";
    //private readonly string pathSetB = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range-calculator\test\testSetB.json";

    private readonly List<ICityDirectString> citiesAsStringSet;
    private readonly ICityDirectString cityDirectString;
    private readonly List<ICity> citySet;

    public CityDirectTest()
    {
        citiesAsStringSet  = JsonReaderWriter.ReadListFromJSON(pathSetA);
        cityDirectString  = JsonReaderWriter.ReadOneFromJSON(pathCityX);
        citySet  = new List<ICity>(citiesAsStringSet);
        
    }

    [TestMethod]
    public void GetFromStringSet_HasCorrectDirectlyReachable()
    {
        var realReachable = CityDirect.GetOneFromStringList(cityDirectString, citiesAsStringSet)
            .DirectlyReachable;

        var shouldBeList = new List<ICity>(citySet).FindAll(c => c.Name == "Bern").OrderBy(x => x);
        
        Assert.IsTrue(realReachable.OrderBy(x => x).SequenceEqual(shouldBeList));
    }

    [TestMethod]
    public void GetSetFromStringSet_ThunAndMeiringenShouldBeThere_And_HaveCorrectICityValues()
    {
        var iCityDirectSet = CityDirect.GetManyFromStringList(citiesAsStringSet);

        // is
        var cityDirectList = new List<ICityDirect>(iCityDirectSet);
        var mayBeThun = cityDirectList.Find( c => c.Name == "Thun" );
        var mayBeMeiringen = cityDirectList.Find( c => c.Name == "Thun" );

        var shouldBeList = new List<ICity>(citySet);
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