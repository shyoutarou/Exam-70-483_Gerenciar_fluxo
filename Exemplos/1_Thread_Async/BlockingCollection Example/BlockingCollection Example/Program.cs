using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockingCollection_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<string> col = new BlockingCollection<string>();

            Task read = Task.Run(() =>
            {
                foreach (string v in col.GetConsumingEnumerable())
                    Console.WriteLine(v);
            });


            ////Task read = Task.Run(() =>
            ////{
            ////    while (true)
            ////    {
            ////        //Se comentar o col.Take() imprimira infinitamente o "oi"
            ////        Console.WriteLine(col.Take());
            ////        Console.WriteLine("oi");
            ////    }
            ////});
            //Task write = Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        string s = Console.ReadLine();
            //        if (string.IsNullOrWhiteSpace(s)) break;
            //        col.Add(s);
            //    }
            //});
            //write.Wait();









            Console.WriteLine("BlockingCollection Com Limite");
            // A blocking collection that can hold no more than 10 items at a time.
            var numberCollection = new BlockingCollection<string>(10);

            Task read_lim = Task.Run(() =>
            {
                while (!numberCollection.IsCompleted)
                {
                    Console.WriteLine("Read " + numberCollection.Take());
                }
                Console.WriteLine("\r\nNo more items to take.");
            });


            // A simple blocking producer with no cancellation.
            Task write_lim = Task.Run(() =>
            {
                foreach (int i in Enumerable.Range(1, 10))
                {
                    Console.WriteLine("adding " + i);
                    numberCollection.Add(i.ToString());
                }

                numberCollection.CompleteAdding();
            });

            write_lim.Wait();
            read_lim.Wait();
            Console.WriteLine("FIM BlockingCollection Com Limite");

            Console.ReadKey();
        }
    }
}
