using System;
using System.Threading;

namespace Prioridade_Thread
{
    class Program
    {
        static bool stop = false;

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(myMethod));
            thread1.Name = "Thread 1";
            thread1.Priority = ThreadPriority.Lowest;

            Thread thread2 = new Thread(new ThreadStart(myMethod));
            thread2.Name = "Thread 2";
            thread2.Priority = ThreadPriority.Highest;

            Thread thread3 = new Thread(new ThreadStart(myMethod));
            thread3.Name = "Thread 3";
            thread3.Priority = ThreadPriority.BelowNormal;

            thread1.Start();
            thread2.Start();
            thread3.Start();
            Thread.Sleep(10000);
            stop = true;

            Console.ReadKey();
        }

        private static void myMethod()
        {
            //Get Name of Current Thread
            string threadName = Thread.CurrentThread.Name.ToString();
            //Get Priority of Current Thread
            string threadPriority = Thread.CurrentThread.Priority.ToString();
            uint count = 0;
            while (stop != true) { count++; }

            Console.WriteLine("{0,-11} with {1,11} priority has a count = {2,13}",
                Thread.CurrentThread.Name, Thread.CurrentThread.Priority.ToString(), count);
        }
    }
}
