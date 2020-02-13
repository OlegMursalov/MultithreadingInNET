using System.Configuration;

namespace ParserAvito.Structure
{
    internal static class ConfigReader
    {
        internal static CianConfigData Execute()
        {
            var mainURI = ConfigurationManager.AppSettings.Get("MainURI");
            var amountOfGoods = int.Parse(ConfigurationManager.AppSettings.Get("AmountOfGoods"));
            var dealType = ConfigurationManager.AppSettings.Get("DealType");
            var engineVersion = int.Parse(ConfigurationManager.AppSettings.Get("EngineVersion"));
            var offerType = ConfigurationManager.AppSettings.Get("OfferType");
            var region = ConfigurationManager.AppSettings.Get("Region");
            var pathToFile = ConfigurationManager.AppSettings.Get("PathToFile");
            return new CianConfigData
            {
                MainURI = mainURI,
                AmountOfGoods = amountOfGoods,
                DealType = dealType,
                EngineVersion = engineVersion,
                OfferType = offerType,
                Region = region,
                PathToFile = pathToFile
            };
        }
    }
}