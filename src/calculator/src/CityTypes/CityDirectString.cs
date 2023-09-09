
using System;
using System.Collections.Generic;
using calculator.JsonInterface;

namespace calculator.CityTypes;

public class CityDirectString : ICityDirectString
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required double[] Location { get; init; }
    public List<string> DirectlyReachable { get; init;} = new List<string>();

    public CityDirectString(ICity c)
    {
        Id = c.Id;
        Name = c.Name;
        Location = c.Location;
    }

    public CityDirectString(CityJson c)
    {
        Id = c.Id;
        Name = c.Name;
        Location = c.Location;
        DirectlyReachable = c.DirectlyReachable;
    }

    public CityJson GetCityJson()
    {
        return new CityJson()
        {
            Id = this.Id,
            Name = this.Name,
            Location = this.Location,
            DirectlyReachable = this.DirectlyReachable
        };
    }

}
