using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlocked_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exemplo_ErroNonatomic();

            //Exemplo_Interlocked();

            CompareExchange_Nonatomic();

            Console.ReadKey();
        }


        static void Exemplo_Interlocked()
        {
            int n = 0;
            object _lock = new object();
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n);
            });
            for (int i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);

            if (Interlocked.Exchange(ref n, 1) == 0) { }

            up.Wait();
            Console.WriteLine(n); //0
        }

        static void Exemplo_ErroNonatomic()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    n++;
            });
            for (int i = 0; i < 1000000; i++)
                n--;
            up.Wait();
            Console.WriteLine(n); //-205 OU o OU 181469
        }

        static void CompareExchange_Nonatomic()
        {
            int value = 1;
            object _lock = new object();
            Task t1 = Task.Run(() =>
            {
                Interlocked.CompareExchange(ref value, 2, 1);

                // OU bloqueio por locks
                //lock (_lock)
                //{
                //    if (value == 1)
                //    {
                //        // Removing the following line will change the output
                //        Thread.Sleep(1000);
                //        value = 2;
                //    }
                //}
            });
            Task t2 = Task.Run(() =>
            {
                //lock (_lock)
                //{
                    value = 3;
                //}
            });
            Task.WaitAll(t1, t2);
            Console.WriteLine(value); // Senão aplicar lock Displays 2, senão 3
        }
    }
}
