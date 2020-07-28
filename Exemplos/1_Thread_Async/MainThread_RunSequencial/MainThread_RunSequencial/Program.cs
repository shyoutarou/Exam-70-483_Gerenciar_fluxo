using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainThread_RunSequencial
{
    class Program
    {
        static void Main(string[] args)
        {
            // We are using Stopwatch to time the code 
            Stopwatch sw = Stopwatch.StartNew();
            // Here we call different methods 
            // for different ways of running our application. 
            RunSequencial();
            RunWithThreads();

            // Print the time it took to run the application. 
            Console.WriteLine("We're done in {0}ms!", sw.ElapsedMilliseconds);

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to continue . . .");
                Console.ReadKey(true);
            }

            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            Console.ReadKey();
        }

        static void RunSequencial()
        {
            double result = 0d;

            // Call the function to read data from I/O 
            result += ReadDataFromIO();

            // Add the result of the second calculation 
            result += DoIntensiveCalculations();

            // Print the result 
            Console.WriteLine("The result is {0}", result);
        }

        static void RunWithThreads()
        {
            double result = 0d;

            // Create the thread to read from I/O 
            var thread = new Thread(() => result = ReadDataFromIO());

            // Start the thread 
            thread.Start();

            // Save the result of the calculation into another variable 
            double result2 = DoIntensiveCalculations();

            // Wait for the thread to finish 
            thread.Join();

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
    }
}
