using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Stop
{
    class Program
    {
        private static bool stopped = false;

        static void Main(string[] args)
        {
            Thread newThread = new Thread(new ThreadStart(TestMethod));
            newThread.Start();
            Thread.Sleep(1000);

            //t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            stopped = true;
            newThread.Join();
        }

        static void TestMethod()
        {
            try
            {
                while (!stopped)
                {
                    Console.WriteLine("New thread running.");
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException abortException)
            {
                Console.WriteLine((string)abortException.ExceptionState);
            }
        }
    }
}
