


using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using calculator.CityTypes;
using calculator.Export;
using calculator.JsonInterface;

namespace calculator.Calculator;

public class Calculator
{
    public string RunningDirectory { get; }
    public string AbsoluteBasePath { get; }
    public string AbsoluteResultPath { get; }

    public Calculator(string relativeBasePath, string relativeResultPath)
    {
        RunningDirectory = Directory.GetCurrentDirectory();
        AbsoluteBasePath = RunningDirectory + relativeBasePath;
        AbsoluteResultPath = RunningDirectory + relativeResultPath;
    }

    public void UpdateFromList(List<CityJson> list)
    {
        JsonReaderWriter.WriteListToJSON(list, AbsoluteBasePath);
    }

    public void Execute(int numberOfSteps)
    {
        try{
            List<ICityDirectString> directStringList = JsonReaderWriter.ReadListFromJSON(AbsoluteBasePath);
            List<ICityDirect> directList = CityDirect.GetManyFromStringList(directStringList);
            List<ICityWithSteps> stepList = CityWithSteps.CreateListFromList(numberOfSteps, directList);
            JsonReaderWriter.WriteStepListToJSON(stepList, AbsoluteResultPath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong: {e.Message}");
        }
    }

    public Task<byte[]> GetResultFileAsPromiseOfByteStream()
    {
        return JsonStreamExporter.GetCitiesWithStringSteps(AbsoluteResultPath);
    }

    public Task<byte[]> GetBaseFileAsPromiseOfByteStream()
    {
        return JsonStreamExporter.GetCitiesWithDirectString(AbsoluteResultPath);
    }
}