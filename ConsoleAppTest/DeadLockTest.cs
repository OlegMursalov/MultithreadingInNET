using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public static class DeadLockTest
    {
        private static object locker1 = new object();
        private static object locker2 = new object();

        public static void Execute()
        {
            Thread t1 = new Thread(Func1);
            Thread t2 = new Thread(Func2);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }

        private static void Func1()
        {
            lock (locker1)
            {
                Console.WriteLine("Func 2 - 1");
                lock (locker2)
                {
                    Console.WriteLine("Func 2 - 2");
                }
            }
        }

        private static void Func2()
        {
            lock (locker2)
            {
                Console.WriteLine("Func 1 - 1");
                lock (locker1)
                {
                    Console.WriteLine("Func 1 - 2");
                }
            }
        }
    }
}