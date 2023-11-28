using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using calculator.CityTypes;

namespace calculator.JsonInterface;
public class JsonReaderWriter
{
    public static CityNameList ReadOneFromJSON(string pathWithFilename)
    // creates city from json file
    {
        var readFromFile = string.Empty;
        using (StreamReader fromFile = new(pathWithFilename))
        {
            readFromFile = fromFile.ReadToEnd();            
        }
        
        // Deserialisation needs a class.
        CityNameList? city;

        try
        {
            city = JsonSerializer.Deserialize<CityNameList>(readFromFile);
        }
        catch (JsonException)
        {
            throw new JsonException("There probably is a typo in the input file...");
        }
        
        return city ??
            throw new JsonReaderWriterException($"ReadOneFromJSON from {pathWithFilename} failed");
    }
    
    public static List<CityNameList> ReadListFromJSON(string pathWithFilename)
    // creates city from json file
    {
        var readFromFile = string.Empty;
        using (StreamReader fromFile = new(pathWithFilename))
        {
            readFromFile = fromFile.ReadToEnd();            
        }
        
        // Deserialisation needs a class. We can use list as it works fine
        // and dublicates will be removed when we create the List.
        List<CityNameList>? list;

        try
        {
            list = JsonSerializer.Deserialize<List<CityNameList>>(readFromFile);
        }
        catch (JsonException)
        {
            throw new JsonException("There probably is a typo in the input file...");
        }
        
        if (list == null)
            throw new JsonReaderWriterException($"ReadSetFromJSON from {pathWithFilename} failed");

        return new List<CityNameList>(list);
    }

    public static void WriteListToJSON(List<CityBasic> list, string pathWithFilename)
    {
        string str = JsonSerializer.Serialize(list, new JsonSerializerOptions() {WriteIndented=true}) ?? string.Empty;

        using StreamWriter toFile = new(pathWithFilename);
        toFile.Write(str);
    }

    public static void WriteListToJSON(List<CityNameList> list, string pathWithFilename)
    {
        string str = JsonSerializer.Serialize(list, new JsonSerializerOptions() {WriteIndented=true}) ?? string.Empty;

        using StreamWriter toFile = new(pathWithFilename);
        toFile.Write(str);
    }

    public static void WriteStepListToJSON(List<CityWithSteps> list, string pathWithFilename)
    {
        List<CityWithStringSteps> simplerList = new();
        list.ForEach( cityWithSteps => simplerList.Add(new CityWithStringSteps(cityWithSteps)));
        string str = JsonSerializer.Serialize(simplerList, new JsonSerializerOptions() {WriteIndented=true}) ?? string.Empty;

        using StreamWriter toFile = new(pathWithFilename);
        toFile.Write(str);
    }
}
