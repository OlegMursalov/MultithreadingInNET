using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public static class CommonResourceBetweenProcessesTest
    {
        public static void Execute()
        {
            FileStream mainFileStream = null;
            try
            {
                mainFileStream = new FileStream(@"C:\commonFile.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                var title = ConfigurationManager.AppSettings.Get("Title");
                WriteToFile(mainFileStream, title, 1000000, (t) => Console.WriteLine(t));
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Запустите от администратора!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (mainFileStream != null)
                {
                    mainFileStream.Dispose();
                }
                Console.ReadKey();
            }
        }

        static void WriteToFile(FileStream mainFileStream, string title, int times, Action<string> postWriteOneTitle)
        {
            for (int i = 0; i < times; i++)
            {
                var bytes = Encoding.UTF8.GetBytes($"{title}\n");
                mainFileStream.Write(bytes, 0, bytes.Length);
                postWriteOneTitle(title);
            }
        }
    }
}
