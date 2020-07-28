using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Thread paused for {0} second", 1);

                // Pause thread for 1 second
                Thread.Sleep(1000);

                Console.WriteLine("i value: {0}", i);
            }


            TimeSpan ts = new TimeSpan(0, 0, 1);

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Thread paused for {0} second", 1);

                // Pause thread for 1 second
                Thread.Sleep(ts);

                Console.WriteLine("i value: {0}", i);
            }

            Console.WriteLine("Main Thread Exists");

            Console.ReadLine();
        }
    }
}
