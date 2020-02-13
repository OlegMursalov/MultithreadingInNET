using ParserAvito.Structure;

namespace ParserAvito
{
    class Program
    {
        static void Main()
        {
            var configData = ConfigReader.Execute();
            var mainParser = new CianMainParser(configData);
            mainParser.Process();
        }
    }
}