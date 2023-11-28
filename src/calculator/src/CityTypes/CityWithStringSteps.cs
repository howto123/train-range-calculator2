



using System.Collections.Generic;

namespace calculator.CityTypes;

public class CityWithStringSteps : CityBasic
{
    public List<string>[] Steps { get; init; }

    public CityWithStringSteps(CityWithSteps city) : base (city)
    {
        Steps = new List<string>[city.Steps.Length];
        var counter = 0;
        foreach( var list in city.Steps)
        {
            var simplerList = new List<string>();
            list.ForEach( c => simplerList.Add(c.Name));
            Steps[counter++] = simplerList;
        }
    }
}