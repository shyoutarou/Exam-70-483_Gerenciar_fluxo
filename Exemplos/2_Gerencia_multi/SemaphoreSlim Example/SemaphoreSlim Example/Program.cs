using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlim_Example
{
    class Program
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3);    // Capacity of 3

        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);

            Console.ReadKey();
        }

        static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter");
            _sem.Wait();
            Console.WriteLine(id + " is in!");           // Only three threads
            Thread.Sleep(1000 * (int)id);               // can be here at
            Console.WriteLine(id + " is leaving");       // a time.
            _sem.Release();
        }
    }
}
