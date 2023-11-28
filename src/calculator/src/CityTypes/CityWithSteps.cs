

using System;
using System.Collections.Generic;

namespace calculator.CityTypes;
public class CityWithSteps : CityDirect
{
    public List<CityBasic>[] Steps { get; init; }

    private CityWithSteps(int numberOfSteps, CityBasic city, List<CityDirect> directList) : base(city)
    {
        // step 0 is the city itself, so we need size n+1 
        Steps = new List<CityBasic>[numberOfSteps+1];

        if ( directList.Find( c => c.Id == city.Id)?.DirectlyReachable?.Contains(city) ?? true)
        {
            throw new Exception($"City {city} could not be created from given list");
        }

        Steps[0] = new List<CityBasic>() { city };
        
        // build step one ad hoc
        var directlyReachable = directList.Find( c => c.Id == city.Id)?.DirectlyReachable;
        var firstStep = new List<CityBasic>() { city } ?? new List<CityBasic>();
        firstStep.AddRange(directlyReachable ?? new List<CityBasic>());
        Steps[1] = firstStep;

        
        for ( int i = 2; i <= numberOfSteps; i++)
        {
            Steps[i] = CreateStepFromLastStep(Steps[i-1], directList);
        }
    }

    private static List<CityBasic> CreateStepFromLastStep(List<CityBasic> lastStep, List<CityDirect> cityList)
    {
        List<CityBasic> result = new (lastStep);
        lastStep.ForEach( last => {

            var reachableFromLast = cityList.Find( c => c.Id == last.Id)?.DirectlyReachable;
            reachableFromLast?.ForEach( maybeNew => {
                if(!result.Contains(maybeNew))
                {
                    result.Add(maybeNew);
                }
            });

        });
        
        return result;
    }

    public static List<CityWithSteps> CreateListFromList (int numberOfSteps, List<CityDirect> directList)
    {
        List<CityWithSteps> result = new();

        foreach( var city in directList )
        {
            result.Add(new CityWithSteps(numberOfSteps, city, directList));
        }

        return result;
    }

    public override string ToString() {
        var stepString = string.Empty;
        var stepNumber = 0;

        foreach( List<CityBasic> list in Steps )
        {
            stepString += $"Step {stepNumber++}: ";
            list?.ForEach( city => stepString += $" {city.Name}");
            stepString += "\r\n";
        }

        return $"{Name}\r\n[{Location[0]}, {Location[1]}]\r\n{stepString}";
    }
}