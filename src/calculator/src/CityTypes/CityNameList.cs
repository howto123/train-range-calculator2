
using System;
using System.Collections.Generic;
using calculator.JsonInterface;

namespace calculator.CityTypes;

public class CityNameList : CityBasic
{
    public List<string> DirectlyReachable { get; init;} = new List<string>();
}
