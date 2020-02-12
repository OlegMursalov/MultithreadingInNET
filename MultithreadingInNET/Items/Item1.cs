using System;
using System.Threading;

namespace MultithreadingInNET.Items
{
    public static class Item1
    {
        public static void Execute()
        {
            // Kick off a new thread
            // running WriteY()
            Thread addedThread = new Thread(WriteY);

            // Приоритет потока означает лишь то,
            // сколько ему будет даваться времени на отработку.
            // Это не значит, что другие потоки, которые имеют меньший приоритет,
            // будут ждать выполнения потока с наивысшим приоритетом,
            // просто им будет даваться меньше времени на отрабтку при переключении
            // контекста.
            addedThread.Priority = ThreadPriority.Lowest;

            addedThread.Start();

            // Simultaneously, do something of the main thread.
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("x");
                if (!addedThread.IsAlive)
                {
                    Console.WriteLine("\nAdded thread is finished!");
                    break;
                }
            } 

            Console.ReadKey();
        }

        public static void WriteY()
        {
            for (int i = 0; i < 1000; i++)
                Console.Write("y");
        }
    }
}