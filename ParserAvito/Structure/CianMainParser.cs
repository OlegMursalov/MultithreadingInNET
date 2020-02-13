using CsQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ParserAvito.Structure
{
    internal class CianMainParser
    {
        private string _mainURI;
        private int _amountOfGoods;
        private string _dealType;
        private int _engineVersion;
        private string _offerType;
        private string _region;
        private string _pathToFile;

        internal CianMainParser(CianConfigData configData)
        {
            _mainURI = configData.MainURI;
            _amountOfGoods = configData.AmountOfGoods;
            _dealType = configData.DealType;
            _engineVersion = configData.EngineVersion;
            _offerType = configData.OfferType;
            _region = configData.Region;
            _pathToFile = configData.PathToFile;
        }

        internal void Process()
        {
            Thread t = new Thread(MainLogic);
            t.Priority = ThreadPriority.AboveNormal;
            t.Start();
            t.Join();
            Console.ReadKey();
        }

        /// <summary>
        /// Данный метод необходимо тщательно проверять! Он сырой!
        /// </summary>
        internal void MainLogic()
        {
            FileStream mainFileStream = null;
            try
            {
                mainFileStream = new FileStream(_pathToFile, FileMode.Append, FileAccess.Write, FileShare.Write);
                for (int i = 0; i < _amountOfGoods; i++)
                {
                    var html = GetHTML(pageNumber: i);
                    var cq = CQ.Create(html);
                    IEnumerable<IDomObject> result = cq.Find("div.c6e8ba5398--single_title--22TGT");
                    foreach (var div in result)
                    {
                        var title = div.FirstChild.ToString();
                        var goodInfo = GetGood(title);
                        SaveGoodInfoToFile(mainFileStream, goodInfo);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Запустите от администратора!");
            }
            finally
            {
                if (mainFileStream != null)
                {
                    mainFileStream.Dispose();
                }
            }
        }

        internal void SaveGoodInfoToFile(FileStream mainFileStream, GoodInfoDTO goodInfoDTO)
        {
            var sb = new StringBuilder();
            sb.AppendLine("- - - - -");
            sb.AppendLine($"Title = {goodInfoDTO.Title}");
            sb.AppendLine("- - - - -");
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            mainFileStream.Write(bytes, 0, bytes.Length);
        }

        internal GoodInfoDTO GetGood(string title)
        {
            return new GoodInfoDTO
            {
                Title = title
            };
        }

        internal string GetHTML(int pageNumber)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; …) Gecko/20100101 Firefox/55.0");
                webClient.Encoding = Encoding.UTF8;
                return webClient.DownloadString($"{_mainURI}?deal_type={_dealType}&engine_version={_engineVersion}&offer_type={_offerType}&region={_region}&p={pageNumber}");
            }
        }
    }
}