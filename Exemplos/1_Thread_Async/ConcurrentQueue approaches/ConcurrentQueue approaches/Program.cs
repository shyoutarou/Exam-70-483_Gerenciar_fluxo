using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentQueue_approaches
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegularQueue_singlethread();

            //ERRO: A matriz de destino não era longa o suficiente. 
            // Verifique destIndex e o comprimento, bem como os limites inferiores da matriz.'
            //for (int i = 0; i < 20; i++)
            //{
            //    RegularQueue_Multithread();
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    LockedQueueAsync();
            //}

            for (int i = 0; i < 50; i++)
            {
                ConcurrentQueueAsync();
            }

            Console.ReadKey();
        }

        static void RegularQueue_singlethread()
        {
            var phoneOrders = new Queue<string>();
            GetOrders("Prakash", phoneOrders);
            GetOrders("Aradhana", phoneOrders);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }

        static void RegularQueue_Multithread()
        {
            var phoneOrders = new Queue<string>();
            Task t1 = Task.Run(() => GetOrders("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrders("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }

        static void LockedQueueAsync()
        {
            var phoneOrders = new Queue<string>();
            Task t1 = Task.Run(() => GetOrdersWithLock("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrdersWithLock("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }

        public static void ConcurrentQueueAsync()
        {
            var phoneOrders = new ConcurrentQueue<string>();
            Task t1 = Task.Run(() => GetOrders("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrders("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);

                Console.WriteLine("Total orders before Dequeue/TryPeek are: {0}", phoneOrders.Count);

                string myOrder;
                if (phoneOrders.TryPeek(out myOrder))    //TryPeek  
                    Console.WriteLine("Order \"{0}\" has been retrieved", myOrder);

                //TryDequeue, Deletes the item from beginning of queue.  
                if (phoneOrders.TryDequeue(out myOrder))
                    Console.WriteLine("Order \"{0}\" has been removed", myOrder);
                else
                    Console.WriteLine("Order queue is empty", myOrder);

                Console.WriteLine("Total orders after Dequeue/TryPeek are: {0}", phoneOrders.Count);
            }
        }

        private static void GetOrders(string custName, object phoneOrders)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(100);
                string order = string.Format("{0} needs {1} phones", custName, i + 5);

                if (phoneOrders is ConcurrentQueue<string>)
                    (phoneOrders as ConcurrentQueue<string>).Enqueue(order);
                else if (phoneOrders is Queue<string>)
                    (phoneOrders as Queue<string>).Enqueue(order);
            }
        }

        static object lockObj = new object();
        private static void GetOrdersWithLock(string custName, Queue<string> phoneOrders)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(100);
                string order = string.Format("{0} needs {1} phones", custName, i + 5);
                lock (lockObj)
                {
                    phoneOrders.Enqueue(order);
                }
            }
        }

    }
}
