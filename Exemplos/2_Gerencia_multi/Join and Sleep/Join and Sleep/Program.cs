using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Join_and_Sleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromHours(1));  // sleep for 1 hour
            Thread.Sleep(500);                     // sleep for 500 milliseconds

            Thread t = new Thread(Go);
            t.Start();
            t.Join();
        }

        static void Go()
        {
            for (int i = 0; i < 1000; i++) Console.Write("y");
        }
    }
}
