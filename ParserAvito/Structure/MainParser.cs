using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
                var htmlItemLines = Regex.Matches(htmlStr, /sdfgafdh/);
                if (htmlItemLines.Count > 0)
                {
                    foreach (var item in htmlItemLines)
                    {
                        if (item is string htmlItemLine)
                        {
                            var goodInfo = GetGood(htmlItemLine);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        internal GoodInfoDTO GetGood(string htmlItemLine)
        {
            return new GoodInfoDTO
            {
                Title = "",
                Price = 434
            };
        }

        internal string GetHTML(int pageNumber)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; …) Gecko/20100101 Firefox/55.0");
                webClient.Encoding = Encoding.UTF8;
                return webClient.DownloadString($"{_mainURI}?q={_query}&p={pageNumber}");
            }
        }
    }
}