using System.Net;
using System.Threading;

namespace ParserAvito.Structure
{
    internal class MainParser
    {
        private string _mainURI;
        private int _amountOfGoods;
        private string _query;
        private string _pathToFile;

        internal MainParser(ConfigData configData)
        {
            _mainURI = configData.MainURI;
            _amountOfGoods = configData.AmountOfGoods;
            _query = configData.Query;
            _pathToFile = configData.PathToFile;
        }

        internal MainParser(string mainURI, int amountOfGoods, string query, string pathToFile)
        {
            _mainURI = mainURI;
            _amountOfGoods = amountOfGoods;
            _query = query;
            _pathToFile = pathToFile;
        }

        internal void Process()
        {
            Thread t = new Thread(MainLogic);
            t.Priority = ThreadPriority.AboveNormal;
            t.Start();
        }

        internal void MainLogic()
        {
            for (int i = 0; i < _amountOfGoods; i++)
            {
                var htmlStr = GetHTML(pageNumber: i);
            }
        }

        internal string GetHTML(int pageNumber)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString($"{_mainURI}?q={_query}&p={pageNumber}");
            }
        }
    }
}