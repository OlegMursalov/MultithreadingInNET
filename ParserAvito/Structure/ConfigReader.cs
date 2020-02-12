using System.Configuration;

namespace ParserAvito.Structure
{
    internal static class ConfigReader
    {
        internal static ConfigData Execute()
        {
            var mainURI = ConfigurationManager.AppSettings.Get("MainURI");
            var amountOfGoods = int.Parse(ConfigurationManager.AppSettings.Get("AmountOfGoods"));
            var query = ConfigurationManager.AppSettings.Get("Query");
            var pathToFile = ConfigurationManager.AppSettings.Get("PathToFile");
            return new ConfigData
            {
                MainURI = mainURI,
                AmountOfGoods = amountOfGoods,
                Query = query,
                PathToFile = pathToFile
            };
        }
    }
}