



using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Export;

public class JsonStreamExporter
{
    public static Task<byte[]> GetCityWithStringSteps()
    {
        string path = FileHandler.GetJsonPath();
        return File.ReadAllBytesAsync(path);
    }
}