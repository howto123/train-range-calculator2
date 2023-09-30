



using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using calculator.CityTypes;
using calculator.JsonInterface;

namespace calculator.Export;

public class JsonStreamExporter
{
    public static Task<byte[]> GetCitiesWithStringSteps()
    {
        string path = FileHandler.GetJsonPath();
        return File.ReadAllBytesAsync(path);
    }

    public static Task<byte[]> GetCitiesWithDirectString()
    {
        string path = FileHandler.GetCityDirectJsonPath();
        return File.ReadAllBytesAsync(path);
    }
}