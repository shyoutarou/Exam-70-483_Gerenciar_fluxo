using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Join
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate a thread
            Thread myThread = new Thread(new ThreadStart(MyThreadMethod));
            //Start the execution of thread
            myThread.Start();
            //Wait until mythread is terminated
            myThread.Join();

            //Everything else is part of Main Thread.
            Console.WriteLine("Hello From Main Thread");

            Console.ReadKey();
        }

        static void MyThreadMethod()
        {
            Console.WriteLine("Hello From My Custom Thread");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Custom Thread");
        }
    }
}
