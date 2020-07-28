using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventWaitHandle_Cross
{
    class Program
    {
        static ManualResetEvent _starter = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.AutoReset, "MyCompany.MyApp.SomeName");

            RegisteredWaitHandle reg = ThreadPool.RegisterWaitForSingleObject
                            (_starter, Go, "Some Data", -1, true);
            Thread.Sleep(5000);
            Console.WriteLine("Signaling worker...");
            _starter.Set();
            Console.ReadLine();
            reg.Unregister(_starter);    // Clean up when we’re done.
        }

        public static void Go(object data, bool timedOut)
        {
            Console.WriteLine("Started - " + data);
            // Perform task...
        }

        void AppServerMethod()
        {
            _starter.WaitOne();
            // ... continue execution
        }


        void AppServerMethod2()
        {
            RegisteredWaitHandle reg = ThreadPool.RegisterWaitForSingleObject
                            (_starter, Resume, null, -1, true);
        }

        static void Resume(object data, bool timedOut)
        {
            // ... continue execution
        }
    }
}
