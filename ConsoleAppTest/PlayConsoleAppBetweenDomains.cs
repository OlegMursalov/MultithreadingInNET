using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class PlayConsoleAppBetweenDomains : IDisposable
    {
        private string _nameConsoleApp;
        private const string MutexMainName = "oleg.mursalov.testApp PlayConsoleAppBetweenDomains";
        private Mutex _mainMutex;

        public PlayConsoleAppBetweenDomains(string nameConsoleApp)
        {
            _nameConsoleApp = nameConsoleApp;
            _mainMutex = new Mutex(false, MutexMainName);
        }

        public void Execute()
        {
            while (true)
            {
                try
                {
                    _mainMutex.WaitOne();
                }
                catch (AbandonedMutexException)
                {
                    _mainMutex.ReleaseMutex();
                    _mainMutex.WaitOne();
                }
                Console.WriteLine($"Приложение {_nameConsoleApp} забрало Mutex");
                while (true)
                {
                    Console.WriteLine($"Работает приложение {_nameConsoleApp}");
                    Console.WriteLine("Нажмите Escape, чтобы выйти");
                    var c = Console.ReadKey();
                    if (c.Key == ConsoleKey.Escape)
                    {
                        _mainMutex.ReleaseMutex();
                        Console.WriteLine($"Приложение {_nameConsoleApp} освободило Mutex");
                        break;
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_mainMutex != null)
            {
                _mainMutex.ReleaseMutex();
            }
        }
    }
}
