using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent_Example
{
    public class Process
    {
        AutoResetEvent auto;
        public Process()
        {
            auto = new AutoResetEvent(false);
            Thread t1 = new Thread(new ThreadStart(akshay));
            Thread t2 = new Thread(new ThreadStart(csharpcorner));
            t1.Start();
            t2.Start();
            auto.Set();
            Thread.Sleep(1000);
            auto.Set();
        }
        void akshay()
        {
            auto.WaitOne();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Akshay Teotia");
            }
        }
        void csharpcorner()
        {
            auto.WaitOne();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("csharpcorner.com");
            }
        }
    }

    class Program
    {
        static EventWaitHandle _waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Process p = new Process();


            new Thread(Waiter).Start();
            Thread.Sleep(1000);                  // Pause for a second...
            _waitHandle.Set();                    // Wake up the Waiter.


            Console.Read();
        }

        static void Waiter()
        {
            Console.WriteLine("Waiting...");
            _waitHandle.WaitOne();                // Wait for notification
            Console.WriteLine("Notified");
        }
    }
}
