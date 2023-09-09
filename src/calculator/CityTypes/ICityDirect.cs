using System;
using System.Collections.Generic;

namespace calculator.CityTypes;

public interface ICityDirect : ICity
{
    List<ICity> DirectlyReachable { get; init;}
}