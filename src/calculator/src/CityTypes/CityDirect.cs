
using System;
using System.Collections.Generic;
using calculator.Exceptions;
using calculator.JsonInterface;

namespace calculator.CityTypes;

public class CityDirect : ICityDirect
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public double[] Location { get; init; }
    public List<ICity> DirectlyReachable { get; init;} = new List<ICity>();

    public CityDirect(ICity c)
    {
        Id = c.Id;
        Name = c.Name;
        Location = c.Location;
    }

    public static ICityDirect GetOneFromStringList(
        ICityDirectString cityDirectString,
        List<ICity> cityList)
    {
       
        ICityDirect result = new CityDirect(cityDirectString);

        cityDirectString.DirectlyReachable?.ForEach( nameString => {
            var cityBelongingToName = cityList.Find( c => c.Name == nameString)
                ?? throw new CityNotFoundException(nameString);
                
            result.DirectlyReachable.Add(cityBelongingToName);
        });

        return result;
    }

    public static ICityDirect GetOneFromStringList(
        ICityDirectString cityDirectString,
        List<ICityDirectString> cityList)
    {
        var simpleList = new List<ICity>();
        cityList.ForEach( c => simpleList.Add(c));
        return GetOneFromStringList(cityDirectString, simpleList);
    }

    public static List<ICityDirect> GetManyFromStringList(List<ICityDirectString> stringList)
    {
        var result = new List<ICityDirect>();

        stringList.ForEach( c => {
            var cityDirect = GetOneFromStringList(c, stringList);
            result.Add(cityDirect);
        });

        return result;
    }

    public override string ToString()
    {
        string reachable = string.Empty;

        DirectlyReachable.ForEach( c => 
            reachable += $"{c.Name}, [{c.Location[0]}, {c.Location[1]}]\r\n"
        );

        return $@"
Id: {Id}
Name: {Name}
Location: [{Location[0]}, {Location[1]}]
DirectlyReachable:
{reachable}";


    }

    public static bool CompareICities(ICity one, ICity two)
    {
        return one.Id == two.Id
            && one.Name == two.Name
            && one.Location == two.Location;
    }
}