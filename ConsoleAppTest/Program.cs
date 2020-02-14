using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main()
        {
            var nameConsoleApp = ConfigurationManager.AppSettings.Get("nameConsoleApp");
            using (var mainLogicApp = new PlayConsoleAppBetweenDomains(nameConsoleApp))
            {
                mainLogicApp.Execute();
            }
        }
    }
}