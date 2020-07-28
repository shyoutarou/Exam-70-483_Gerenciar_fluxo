using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Invoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Step1(); Step2(); Step3(); //Step1  Step2  Step3
            Parallel.Invoke(Step1, Step2, Step3); //Step2  Step3  Step1

            Console.ReadKey();
        }

        static void Step1() { Thread.Sleep(1000); Console.WriteLine("Step1"); }
        static void Step2() { Thread.Sleep(200); Console.WriteLine("Step2"); }
        static void Step3() { Thread.Sleep(800); Console.WriteLine("Step3"); }
    }
}
