using System.Collections.Generic;

namespace calculator.CityTypes;


public interface ICityWithSteps : ICityDirect
{
    public List<ICity>[] Steps { get; init; }
}