

namespace calculator.Export
{
    public class FileHandler
    {
        public static string GetJsonPath()
        {
            //path from project root, NOT relative to this file
            return "./src/data/citiesWithStringSteps.json";
        }

        public static string GetCityDirectJsonPath()
        {
            return "./src/data/citiesWithDirectStrings.json";
        }
    }
}