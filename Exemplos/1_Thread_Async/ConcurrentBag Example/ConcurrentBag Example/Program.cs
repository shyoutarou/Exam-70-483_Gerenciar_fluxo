using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentBag_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            bag.Add(42);
            bag.Add(21);
            int result;
            if (bag.TryTake(out result))
                Console.WriteLine("TryTake: " + result);
            if (bag.TryPeek(out result))
                Console.WriteLine("There is a next item: {0}", result);


            ConcurrentBag<int> bag2 = new ConcurrentBag<int>();
            Task.Run(() =>
            {
                bag2.Add(42);
                Thread.Sleep(1000); // Se comentar aqui, aparece 0 21 e 42
                bag2.Add(21);
            });
            Task.Run(() =>
            {
                foreach (int i in bag2)
                    Console.WriteLine(i);
            }).Wait();


            Console.ReadKey();
        }
    }
}
