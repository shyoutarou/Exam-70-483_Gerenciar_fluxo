using System;
using System.Threading;

namespace Mutex_Example
{
    class Program
    {
        // Create a new Mutex. The creating thread does not own the mutex.
        private static Mutex mut = new Mutex();
        private const int numIterations = 1;
        private const int numThreads = 3;


        static void Main(string[] args)
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).
            using (var mutex = new Mutex(false, "oreilly.com OneAtATimeDemo"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.
                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another app instance is running. Bye!");
                    return;
                }
                RunProgram();
            }




            // Create the threads that will use the protected resource.
            for (int i = 0; i < numThreads; i++)
            {
                Thread newThread = new Thread(new ThreadStart(ThreadProc));
                newThread.Name = String.Format("Thread{0}", i + 1);
                newThread.Start();
            }
        }

        static void RunProgram()
        {
            Console.WriteLine("Running. Press Enter to exit");
            Console.ReadLine();
        }





        private static void ThreadProc()
        {
            for (int i = 0; i < numIterations; i++)
            {
                UseResource();
            }
        }

        // This method represents a resource that must be synchronized
        // so that only one thread at a time can enter.
        private static void UseResource()
        {

            // Wait until it is safe to enter.
            Console.WriteLine("{0} is requesting the mutex", Thread.CurrentThread.Name);
            mut.WaitOne();

            Console.WriteLine("{0} has entered the protected area", Thread.CurrentThread.Name);

            // Place code to access non-reentrant resources here.

            // Simulate some work.
            Thread.Sleep(500);

            Console.WriteLine("{0} is leaving the protected area", Thread.CurrentThread.Name);

            // Release the Mutex.
            mut.ReleaseMutex();
            Console.WriteLine("{0} has released the mutex", Thread.CurrentThread.Name);
        }
    }
}
