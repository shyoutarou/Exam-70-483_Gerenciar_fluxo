﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEvent_Simple
{
    class Program
    {
        static CountdownEvent _countdown = new CountdownEvent(3);

        static void Main(string[] args)
        {
            new Thread(SaySomething).Start("I am thread 1");
            new Thread(SaySomething).Start("I am thread 2");
            new Thread(SaySomething).Start("I am thread 3");

            _countdown.Wait();   // Blocks until Signal has been called 3 times
            Console.WriteLine("All threads have finished speaking!");
            Console.ReadKey();
        }
        static void SaySomething(object thing)
        {
            Thread.Sleep(1000);
            Console.WriteLine(thing);
            _countdown.Signal();
        }
    }
}
