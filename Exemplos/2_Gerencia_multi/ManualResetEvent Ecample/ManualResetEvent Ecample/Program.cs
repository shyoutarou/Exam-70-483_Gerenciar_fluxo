using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEvent_Ecample
{
    class MyThread
    {
        public Thread th;
        ManualResetEvent manualResetEvent;
        public MyThread(string name, ManualResetEvent e)
        {
            th = new Thread(this.run);
            th.Name = name;
            manualResetEvent = e;
            th.Start();
        }
        void run()
        {
            Console.WriteLine("Inside thread " + th.Name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(th.Name);
                Thread.Sleep(50);
            }
            Console.WriteLine(th.Name + " Done!");
            manualResetEvent.Set();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ManualResetEvent evtObj = new ManualResetEvent(false);
            MyThread myThread = new MyThread("Event Thread 1", evtObj);
            Console.WriteLine("Main thread waiting for event.");
            // Wait for signaled event.
            evtObj.WaitOne();
            Console.WriteLine("Main thread received first event.");
            evtObj.Reset();
            myThread = new MyThread("Event Thread 2", evtObj);
            // Wait for signaled event.
            evtObj.WaitOne();
            Console.WriteLine("Main thread received second event.");
            Console.Read();
        }
    }
}
