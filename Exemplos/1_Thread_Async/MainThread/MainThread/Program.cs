using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Explicito_ThreadStart();
            //Implicito_ThreadStart();

            //Parameterized_ThreadStart();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }

            Console.ReadKey();

        }

        static void Explicito_ThreadStart()
        {
            Thread myThread = new Thread(new ThreadStart(MyThreadMethod));
            myThread.Start();

            Console.WriteLine("Hello From Main Thread");
        }

        static void Implicito_ThreadStart()
        {
            //Create a new thread and execute the WriteX method on the thread
            Thread t = new Thread(voidMethod);
            t.Start();

            // This will run on the main thread
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("O");
            }

            Thread myThread = new Thread(MyThreadMethod_Param);
            myThread.Start(20);

            //Instantiate a thread
            Thread myThread_param = new Thread(new ParameterizedThreadStart(MyThreadMethod_Param));
            //Start the execution of thread
            myThread_param.Start(50);
        }

        static void Parameterized_ThreadStart()
        {
            var th = new Thread(ExecuteInForeground);
            th.Start();
            Thread.Sleep(1000);

            var th_param = new Thread(ExecuteInForeground_Param);
            th_param.Start(4500);
            Thread.Sleep(1000);
        }

        // This will run on the new thread The processor will allocate 
        // slices of time to each thread.Usually around 20ms each
        static void voidMethod()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("x");
            }
        }

        static void MyThreadMethod()
        {
            Console.WriteLine("Hello From My Custom Thread");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" {0} ", i);
                Thread.Sleep(0);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Custom Thread");
        }

        static void MyThreadMethod_Param(Object param)
        {
            Console.WriteLine("Hello From My Custom Thread");
            for (int i = 0; i < (int)param; i++)
            {
                Console.Write(" {0} ", i);
                Thread.Sleep(0);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Custom Thread");
        }


        private static void ExecuteInForeground()
        {
            DateTime start = DateTime.Now;
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Thread {0}: {1}, Priority {2}",
                              Thread.CurrentThread.ManagedThreadId,
                              Thread.CurrentThread.ThreadState,
                              Thread.CurrentThread.Priority);
            do
            {
                Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                                  Thread.CurrentThread.ManagedThreadId,
                                  sw.ElapsedMilliseconds / 1000.0);
                Thread.Sleep(500);
            } while (sw.ElapsedMilliseconds <= 5000);
            sw.Stop();

            Console.Write("===============================");
        }


        private static void ExecuteInForeground_Param(Object obj)
        {
            int interval;
            try
            {
                interval = (int)obj;
            }
            catch (InvalidCastException)
            {
                interval = 5000;
            }
            DateTime start = DateTime.Now;
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Thread {0}: {1}, Priority {2}",
                              Thread.CurrentThread.ManagedThreadId,
                              Thread.CurrentThread.ThreadState,
                              Thread.CurrentThread.Priority);
            do
            {
                Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                                  Thread.CurrentThread.ManagedThreadId,
                                  sw.ElapsedMilliseconds / 1000.0);
                Thread.Sleep(500);
            } while (sw.ElapsedMilliseconds <= interval);
            sw.Stop();
        }
    }
}
