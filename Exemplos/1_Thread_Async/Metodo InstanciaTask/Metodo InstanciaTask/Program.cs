using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metodo_InstanciaTask
{
    class Program
    {
        static void Main(string[] args)
        {




            //initialize mytask and assign a unit of work in form of 'myMethod()'
            Task myTask = new Task(myMethod);
            myTask.Start();// Start the execution of mytask
            myTask.Wait(); //Wait until mytask finish its job

            Console.WriteLine("Bye From Main Thread");


            Task<int> myTaskreturnvalue = new Task<int>(myMethodreturnvalue);
            myTaskreturnvalue.Start(); //start myTask
            Console.WriteLine("Hello from Main Thread returns value");
            //Wait the main thread until myTaskreturnvalue is finished
            //and returns the value from myTaskreturnvalue operation (myMethod)
            int i = myTaskreturnvalue.Result;
            Console.WriteLine("myTask has a return value = {0}", i);

            Console.WriteLine("Task id: {0}", Task.C.CurrentId); // 1

            Console.WriteLine("Bye From Main Thread returns value");

            Console.ReadKey();
        }

        static int myMethodreturnvalue()
        {
            Console.WriteLine("Hello from myTask<int>");
            Console.WriteLine("Task id: {0}", Task.CurrentId); // 1
            Thread.Sleep(1000);
            return 10;
        }

        private static void myMethod()
        {
            Console.WriteLine("Hello From My Task");
            Console.WriteLine("Task id: {0}", Task.CurrentId); // 1
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Task");
        }
    }
}
