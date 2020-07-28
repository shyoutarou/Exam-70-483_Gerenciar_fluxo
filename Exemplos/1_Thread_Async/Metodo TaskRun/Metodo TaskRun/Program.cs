using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metodo_TaskRun
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize and Run mytask and assign
            //a unit of work in form of 'myMethod()'
            Task mytask = Task.Run(new Action(myMethod));
            mytask.Wait(); //Wait until mytask finish its job

            Task mylamnbdaTask = Task.Run(() =>
                                {
                                    Console.WriteLine("Hello From My Task");
                                    for (int i = 0; i < 10; i++)
                                    {
                                        Console.Write("{0} ", i);
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("Bye From My Task");
                                });

            mylamnbdaTask.Wait(); //Wait until mytask finish its job

            Console.WriteLine("Hello From Main Thread lamnbda");



            Task<int> myTaskreturnvalue = Task.Run<int>(new Func<int>(myMethodreturnvalue));
            Console.WriteLine("Hello from Main Thread returns value");
            //Wait for the main thread until myTask is finished
            //and return the value from myTask operation (myMethod)
            int valor = myTaskreturnvalue.Result;
            Console.WriteLine("myTaskreturnvalue has a return value = {0}", valor);
            Console.WriteLine("Bye From Main Thread returns value");



            Task<int> mylamnbdaTaskreturnvalue = Task.Run<int>(() =>
                                    {
                                        Console.WriteLine("Hello from myTask<int>");
                                        Thread.Sleep(1000);
                                        return 10;
                                    });


            var myTask = Task.Run(() =>
                                    {
                                        Console.WriteLine("Hello from myTask<int>");
                                        Thread.Sleep(1000);
                                        return 10;
                                    });

            Console.WriteLine("Hello From Main Thread lamnbda return value");

            //Wait for the main thread until myTask is finished
            //and return the value from myTask operation
            int valorlamnbda = mylamnbdaTaskreturnvalue.Result;
            Console.WriteLine("mylamnbdaTaskreturnvalue has a return value = {0}", valorlamnbda);
            valorlamnbda = myTask.Result;
            Console.WriteLine("mylamnbdaTaskreturnvalue has a return value = {0}", valorlamnbda);
            Console.WriteLine("Bye From Main Thread lamnbda returns value");

            Console.ReadKey();
        }

        static int myMethodreturnvalue()
        {
            Console.WriteLine("Hello from myTask<int>");
            Thread.Sleep(1000);
            return 10;
        }

        private static void myMethod()
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
