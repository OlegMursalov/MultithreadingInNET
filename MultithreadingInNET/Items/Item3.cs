using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingInNET.Items
{
    public class Item3
    {
        bool done;

        internal static void Execute()
        {
            Item3 tt = new Item3(); // Create a common instance
            new Thread(tt.Go).Start();
            tt.Go();

            Console.ReadKey();
        }

        // Note that Go is now an instance method
        void Go()
        {
            // Потоки все равно могут зайти сюда,
            // а не только один.
            // Мы же не сечем, как конкретно работает
            // переключатель контекста.
            if (!done)
            {
                Console.WriteLine("Done");
                done = true;
            }
        }
    }
}
