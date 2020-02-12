using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingInNET.Items
{
    /// <summary>
    /// Пора зашарить за эксклюзивную блокировку.
    /// Потоки должны понимать, когда все должны стопнуться и ждать выполнения 
    /// другого потока.
    /// </summary>
    public static class Item4
    {
        static bool done;
        static readonly object locker = new object();

        /// <summary>
        /// Да, я ошибся. Если указать приоритет потока Lowest, то, вероятно,
        /// что он выполнится именно последним по сравнению с другими потоками.
        /// </summary>
        public static void Execute()
        {
            ParameterizedThreadStart xMethod = (c) => Go((char)c);

            var t1 = new Thread(xMethod);
            t1.Priority = ThreadPriority.BelowNormal;
            t1.Start('a');

            var t2 = new Thread(xMethod);
            t2.Priority = ThreadPriority.Lowest;
            t2.Start('b');

            var t3 = new Thread(xMethod);
            t3.Priority = ThreadPriority.Highest;
            t3.Start('c');

            Console.ReadKey();
        }

        private static void Go(char c)
        {
            lock (locker)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.Write(c);
                }
            }
        }
    }
}
