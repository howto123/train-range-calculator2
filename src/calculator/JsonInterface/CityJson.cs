using System;
using System.Collections.Generic;
using calculator.CityTypes;

namespace calculator.JsonInterface;

public class CityJson : ICityDirectString
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; init; }
    public required double[] Location { get; init; }

    public List<string> DirectlyReachable { get; init;} = new List<string>();


    public override string ToString()
    {
        string result =
$@"
ID: {Id}
Name: {Name}
Location: [{Location[0]}, {Location[1]}]
DirectlyReachable: ";

        foreach (var item in DirectlyReachable)
        {
            result += $"{item}  ";
        }
        return result;
    }
}
