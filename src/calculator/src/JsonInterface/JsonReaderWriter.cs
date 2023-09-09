using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using calculator.CityTypes;

namespace calculator.JsonInterface;
public class JsonReaderWriter
{
        public static ICityDirectString ReadOneFromJSON(string pathWithFilename)
    // creates city from json file
    {
        var readFromFile = string.Empty;
        using (StreamReader fromFile = new(pathWithFilename))
        {
            readFromFile = fromFile.ReadToEnd();            
        }
        
        // Deserialisation needs a class.
        CityJson? city;

        try
        {
            city = JsonSerializer.Deserialize<CityJson>(readFromFile);
        }
        catch (JsonException)
        {
            throw new JsonException("There probably is a typo in the input file...");
        }
        
        return city ??
            throw new JsonReaderWriterException($"ReadOneFromJSON from {pathWithFilename} failed");
    }
    public static List<ICityDirectString> ReadListFromJSON(string pathWithFilename)
    // creates city from json file
    {
        var readFromFile = string.Empty;
        using (StreamReader fromFile = new(pathWithFilename))
        {
            readFromFile = fromFile.ReadToEnd();            
        }
        
        // Deserialisation needs a class. We can use list as it works fine
        // and dublicates will be removed when we create the List.
        List<CityJson>? list;

        try
        {
            list = JsonSerializer.Deserialize<List<CityJson>>(readFromFile);
        }
        catch (JsonException)
        {
            throw new JsonException("There probably is a typo in the input file...");
        }
        
        if (list == null)
            throw new JsonReaderWriterException($"ReadSetFromJSON from {pathWithFilename} failed");

        return new List<ICityDirectString>(list);
    }

    public static void WriteListToJSON(List<ICity> list, string pathWithFilename)
    {
        string str = JsonSerializer.Serialize(list, new JsonSerializerOptions() {WriteIndented=true}) ?? string.Empty;

        using StreamWriter toFile = new(pathWithFilename);
        toFile.Write(str);
    }

    public static void WriteStepListToJSON(List<ICityWithSteps> list, string pathWithFilename)
    {
        List<CityWithStringSteps> simplerList = new();
        list.ForEach( cityWithSteps => simplerList.Add(new CityWithStringSteps(cityWithSteps)));
        string str = JsonSerializer.Serialize(simplerList, new JsonSerializerOptions() {WriteIndented=true}) ?? string.Empty;

        using StreamWriter toFile = new(pathWithFilename);
        toFile.Write(str);
    }
}
