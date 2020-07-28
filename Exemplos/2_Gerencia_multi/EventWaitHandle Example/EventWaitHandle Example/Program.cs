using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventWaitHandle_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RunInThreadPool();
            RunInThreadPoolWithEvents();

            Console.ReadKey();
        }

        static void RunInThreadPool()
        {
            double result = 0d;
            // Create a work item to read from I/O
            ThreadPool.QueueUserWorkItem((x) => result += ReadDataFromIO());
            // Save the result of the calculation into another variable
            double result2 = DoIntensiveCalculations();
            // Wait for the thread to finish
            // TODO: We will need a way to indicate
            // when the thread pool thread finished the execution
            // Calculate the end result
            result += result2;
            // Print the result
            Console.WriteLine("The result is {0}", result);
        }

        static double ReadDataFromIO()
        {
            // We are simulating an I/O by putting the current thread to sleep. 
            Thread.Sleep(5000);
            return 10d;
        }

        static double DoIntensiveCalculations()
        {
            // We are simulating intensive calculations 
            // by doing nonsens divisions 
            double result = 100000000d;
            var maxValue = Int32.MaxValue;
            for (int i = 1; i < maxValue; i++)
            {
                result /= i;
            }

            return result + 10d;
        }

        static void RunInThreadPoolWithEvents()
        {
            double result = 0d;
            // We use this event to signal when the thread is don executing.
            EventWaitHandle calculationDone =
            new EventWaitHandle(false, EventResetMode.ManualReset);
            // Create a work item to read from I/O
            ThreadPool.QueueUserWorkItem((x) =>
            {
                result += ReadDataFromIO();
                calculationDone.Set();
            });
            // Save the result of the calculation into another variable
            double result2 = DoIntensiveCalculations();
            // Wait for the thread to finish
            calculationDone.WaitOne();
            // Calculate the end result
            result += result2;
            // Print the result
            Console.WriteLine("The result is {0}", result);
        }
    }
}
