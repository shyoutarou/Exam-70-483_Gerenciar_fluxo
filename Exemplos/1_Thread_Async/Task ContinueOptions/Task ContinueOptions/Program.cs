using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_ContinueOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> tsk1 = Task.Run(() =>
            {
                throw new Exception();
                Console.WriteLine("tsk1 ran");
                Thread.Sleep(100);
                return "Ali";
            });


            Task<int> t = Task.Run(() =>
            {
                return 42;
            });

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);
            t.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = t.ContinueWith((i) =>
            {
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);




            Task tsk2 = tsk1.ContinueWith((t) =>
            {
                Console.WriteLine("tsk2 ran when tsk1 threw an exception");
            }, TaskContinuationOptions.OnlyOnFaulted);

            tsk2.Wait();

            Console.ReadKey();
        }
    }
}
