

using System;

namespace calculator.CityTypes;
public interface ICity
{
    Guid Id { get; }
    public string Name { get; init; }
    public double[] Location { get; init; }
    
}