using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingInNET.Items
{
    public static class Item5
    {
        private static int millisecondsTimeout = 1000;

        public static void Execute()
        {
            Thread t = new Thread(Go);
            t.Start();

            // Join блокирует текущий поток!
            // Блоканутые потоки не потребляют CPU ресурсы!
            var tResult = t.Join(millisecondsTimeout);

            if (tResult)
            {
                Console.WriteLine("\nПоток завершился сам!");
            }
            else
            {
                Console.WriteLine("\nПоток завершился по timeout (все не выполнил)");
                if (t.IsAlive)
                {
                    Console.WriteLine("Поток t еще жив!");
                }
            }

            Console.ReadKey();
        }

        static void Go()
        {
            for (int i = 0;i < 10000; i++)
            {
                Console.Write('y');
            }
        }
    }
}
