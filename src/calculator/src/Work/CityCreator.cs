using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using calculator.CityTypes;
using calculator.JsonInterface;

namespace calculator.Work;

public class CityCreator
{
    public static void MyPlayGround ()
    {
        Console.WriteLine($"MyPlayGround running!");
        var pathSetA = @"C:\Users\Thinkpad53u\Files\Dev\99 ProjectIdeas\train-range-calculator\test\testSetA.json";
        var stringList = JsonReaderWriter.ReadListFromJSON(pathSetA);
        var directList = CityDirect.GetManyFromStringList(stringList);
        var finalList = CityWithSteps.CreateListFromList(3, directList);
        JsonReaderWriter.WriteStepListToJSON(finalList, "./output.json");
        

        Console.WriteLine($"MyPlayGround finished.");

        // List<ICityDirect> directSet = JsonReaderWriter.getSet("./citiesOneStep.json");
        // List<ICityWithSteps> citiesWithSteps = CityCreator.Create(directSet);
        // JsonReaderWriter.writeCitiesWithSteps("./citiesSteps.json");
    }


}
