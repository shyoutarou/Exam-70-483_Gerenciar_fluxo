using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentStack_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            stack.Push(42);
            int result;
            if (stack.TryPop(out result))
                Console.WriteLine("Popped: {0}", result);
            stack.PushRange(new int[] { 1, 2, 3 });
            int[] values = new int[2];
            stack.TryPopRange(values);
            foreach (int i in values)
                Console.WriteLine(i);


            //RegularStack_IncorrectCount();
            ConcurrentQueue_CorrectCount();

            Console.ReadKey();
        }

        static void RegularStack_IncorrectCount()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000000);
            Stack<int> _stack = new Stack<int>(numbers);

            long _total = 0;

            int value;
            Task task1 = Task.Run(() =>
            {
                while (true)
                {
                    Interlocked.Add(ref _total, _stack.Pop());
                }
            });

            Task task2 = Task.Run(() =>
            {
                while (true)
                {
                    Interlocked.Add(ref _total, _stack.Pop());
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

            Console.WriteLine("Total: {0}", _total); // Total: 499933914565
        }

    }
}
