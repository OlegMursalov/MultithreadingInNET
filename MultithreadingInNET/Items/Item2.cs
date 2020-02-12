using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingInNET.Items
{
    public static class Item2
    {
        /// <summary>
        /// Static fields are shared between all threads
        /// </summary>
        private static int cycles;

        /// <summary>
        /// Локальные переменные разделяются на каждый поток
        /// </summary>
        public static void Execute()
        {
            // Call Go() on a new thread
            new Thread(Go).Start();

            // Call Go() on the main thread
            Go();

            Console.ReadKey();
        }

        static void Go()
        {
            // Declare and use a local variable - 'cycles'
            for (cycles = 0; cycles < 5; cycles++)
                Console.Write('?');
        }
    }
}
