using System;
using System.Collections.Generic;

namespace calculator.CityTypes;

public interface ICityDirectString : ICity
{
    // Id, Name, Location are inherited
    List<string> DirectlyReachable { get; init;}
}
