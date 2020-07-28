using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadStart_Object
{
    public class Work
    {
        public static void StaticMethod(object data)
        {
            Console.WriteLine("StaticMethod is running on another thread. Data='{0}'", data);

            // Pause for a moment to provide a delay to make threads more apparent.
            Thread.Sleep(5000);
            Console.WriteLine("StaticMethod called by the worker thread has ended.");
        }

        public void InstanceMethod(object data)
        {
            Console.WriteLine("InstanceMethod is running on another thread. Data='{0}'", data);

            // Pause for a moment to provide a delay to make threads more apparent.
            Thread.Sleep(3000);
            Console.WriteLine("InstanceMethod called by the worker thread has ended.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Start a thread that calls a parameterized static method.
            Thread newThread = new Thread(Work.StaticMethod);
            newThread.Start(42);

            Work owork = new Work();
            // Start a thread that calls a parameterized instance method.
            newThread = new Thread(owork.InstanceMethod);
            newThread.Start("The answer.");

            Console.ReadKey();
        }
    }
}
