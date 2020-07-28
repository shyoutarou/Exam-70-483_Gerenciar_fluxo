using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metodo_TaskFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize and Start mytask and assign
            //a unit of work in the body of lambda exp
            Task mytask = Task.Factory.StartNew(new Action(myMethod));
            mytask.Wait(); //Wait until mytask finish its job

            Console.WriteLine("Hello From Main Thread");



            Task<int> myTaskreturnvalue = Task<int>.Factory.StartNew(myMethodreturnvalue);
            Console.WriteLine("Hello from Main Thread returns value");
            //Wait the main thread until myTaskreturnvalue is finished
            //and returns the value from myTaskreturnvalue operation (myMethod)
            int i = myTaskreturnvalue.Result;
            Console.WriteLine("myTask has a return value = {0}", i);
            Console.WriteLine("Bye From Main Thread returns value");


            Console.ReadKey();
        }

        static int myMethodreturnvalue()
        {
            Console.WriteLine("Hello from myTask<int>");
            Thread.Sleep(1000);
            return 10;
        }


        static void myMethod()
        {
            Console.WriteLine("Hello From My Task");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Task");
        }
    }
}
