
using System;
using System.Collections.Generic;
using calculator.Exceptions;
using calculator.JsonInterface;

namespace calculator.CityTypes;

public class CityDirect : CityBasic
{
    public List<CityBasic> DirectlyReachable { get; init;} = new List<CityBasic>();

    public CityDirect(CityBasic c)
    {
        Id = c.Id;
        Name = c.Name;
        Location = c.Location;
    }


    // Checks if names correspond to cities
    public static CityDirect CreateValid
    (
        CityNameList cityDirectString,
        List<CityBasic> cityList
    )
    {
       
        CityDirect result = new(cityDirectString);

        cityDirectString.DirectlyReachable?.ForEach( nameString => {
            var cityBelongingToName = cityList.Find( c => c.Name == nameString)
                ?? throw new CityNotFoundException(nameString);
                
            result.DirectlyReachable.Add(cityBelongingToName);
        });

        return result;
    }

    public static CityDirect CreateValid
    (
        CityNameList cityDirectString,
        List<CityNameList> cityList)
    {
        var cityBasicList = new List<CityBasic>();
        cityList.ForEach( c => cityBasicList.Add(c));
        return CreateValid(cityDirectString, cityBasicList);
    }

    public static List<CityDirect> GetManyFromStringList(List<CityNameList> stringList)
    {
        var result = new List<CityDirect>();

        stringList.ForEach( c => {
            var cityDirect = CreateValid(c, stringList);
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

    public static bool CompareICities(CityBasic one, CityBasic two)
    {
        return one.Id == two.Id
            && one.Name == two.Name
            && one.Location == two.Location;
    }
}