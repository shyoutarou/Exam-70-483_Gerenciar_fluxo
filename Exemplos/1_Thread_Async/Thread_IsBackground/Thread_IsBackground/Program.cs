using System;
using System.Diagnostics;
using System.Threading;

namespace Thread_IsBackground
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate a thread
            Thread myThread = new Thread(new ThreadStart(MyThreadMethod));

            //by default Isbackgrount value is always false
            myThread.IsBackground = true;
            //Start the execution of thread
            myThread.Start();

            ////Main Thread wait until mythread terminated
            //myThread.Join();

            Console.WriteLine("Hello From IsBackground_true Thread");
        }

        static void MyThreadMethod()
        {
            var sw = Stopwatch.StartNew();

            Debug.WriteLine("End of MyThread");
            Console.WriteLine("Start of MyThread");
            for (int i = 0; i < 10; i++)
            {
                //suspend the thread for 100 milliseconds
                Thread.Sleep(100);
                Debug.Write(i.ToString() + "\t");
                Console.Write(i.ToString() + "\t");
            }

            Debug.WriteLine("End of MyThread");
            Console.WriteLine();
            Console.WriteLine("End of MyThread");

            sw.Stop();
            Debug.WriteLine("Thread: Elapsed {0:N2} miliseconds", sw.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
