using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            // Queue the thread.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));

            Console.WriteLine("Hello From Main Thread.");
            // The thread pool uses background threads, its important
            // to keep main thread busy otherwise program will terminate
            // before the background thread complete its execution
            Console.ReadLine(); //Wait for Enter
            Console.WriteLine("Hello Again from Main Thread.");
            // Queue the thread with Lambda
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Hi I'm another free thread from thread pool");
            });
            Console.ReadLine(); //Wait for Enter
        }

        // This thread procedure performs the task.
        static void ThreadProc(Object stateInfo)
        {
            // No state object was passed to QueueUserWorkItem, so stateInfo is null.
            Console.WriteLine("Hello from the thread pool.");
        }
    }
}
