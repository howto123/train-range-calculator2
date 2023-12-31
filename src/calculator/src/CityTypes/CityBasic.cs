



using System;

namespace calculator.CityTypes;
public class CityBasic
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = "";
    public double[] Location { get; init; } = { 0.0, 0.0 };

    public CityBasic(string name, double[] location)
    {
        if(location.Length != 2)
            throw new ArgumentException("Location needs two coordinates");
        Name = name;
        Location = location;
    }

    public CityBasic(CityBasic city)
    {
        Id = city.Id;
        Name = city.Name;
        Location = city.Location;
    }

    public CityBasic()
    {
        // default
    }
}