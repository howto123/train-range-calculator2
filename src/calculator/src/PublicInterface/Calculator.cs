


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

    public Calculator(CalculatorSettings settings) : this(
        settings.RelativeBasePath,
        settings.RelativeResultPath
    )
    {
    }

    public void UpdateFromList(List<CityNameList> list)
    {
        JsonReaderWriter.WriteListToJSON(list, AbsoluteBasePath);
    }

    public void Execute(int numberOfSteps)
    {
        try{
            List<CityNameList> directStringList = JsonReaderWriter.ReadListFromJSON(AbsoluteBasePath);
            List<CityDirect> directList = CityDirect.GetManyFromStringList(directStringList);
            List<CityWithSteps> stepList = CityWithSteps.CreateListFromList(numberOfSteps, directList);
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
        return JsonStreamExporter.GetCitiesWithDirectString(AbsoluteBasePath);
    }

    public string GetAbsoluteBasePath()
    {
        return AbsoluteBasePath;
    }

    public string GetAbsoluteResultPath()
    {
        return AbsoluteResultPath;
    }
}