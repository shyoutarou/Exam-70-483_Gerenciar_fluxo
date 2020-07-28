using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
            //queue.Enqueue(42);
            //int result;
            //if (queue.TryDequeue(out result))
            //    Console.WriteLine("Dequeued: {0}", result);


            //RegularStack_IncorrectCount();

            ConcurrentQueue_CorrectCount();



            Console.ReadKey();
        }

        static void RegularStack_IncorrectCount()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000000);
            Queue<int> _queued = new Queue<int>(numbers);
            long _total = 0;

            Task task1 = Task.Run(() =>
            {
                while (true)
                {
                    Interlocked.Add(ref _total, _queued.Dequeue());
                }
            });

            Task task2 = Task.Run(() =>
            {
                while (true)
                {
                    Interlocked.Add(ref _total, _queued.Dequeue());
                }
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine("Total: {0}", _total);
        }

        static void ConcurrentQueue_CorrectCount()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000000);
            ConcurrentQueue<int> _queued = new ConcurrentQueue<int>(numbers);
            long _total = 0;

            int value;
            Task task1 = Task.Run(() =>
            {
                while (_queued.TryDequeue(out value))
                {
                    Interlocked.Add(ref _total, value);
                }
            });

            Task task2 = Task.Run(() =>
            {
                while (_queued.TryDequeue(out value))
                {
                    Interlocked.Add(ref _total, value);
                }
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine("Total: {0}", _total); //Total: 500000288629
        }
    }
}
