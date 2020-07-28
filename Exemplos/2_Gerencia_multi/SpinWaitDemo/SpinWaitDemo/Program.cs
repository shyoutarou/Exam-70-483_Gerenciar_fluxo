using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinWaitDemo
{
    public class LockFreeStack<T>
    {
        private volatile Node m_head;

        private class Node { public Node Next; public T Value; }

        public void Push(T item)
        {
            var spin = new SpinWait();
            Node node = new Node { Value = item }, head;
            while (true)
            {
                head = m_head;
                node.Next = head;
                if (Interlocked.CompareExchange(ref m_head, node, head) == head) break;
                spin.SpinOnce();
            }
        }

        public bool TryPop(out T result)
        {
            result = default(T);
            var spin = new SpinWait();

            Node head;
            while (true)
            {
                head = m_head;
                if (head == null) return false;
                if (Interlocked.CompareExchange(ref m_head, head.Next, head) == head)
                {
                    result = head.Value;
                    return true;
                }
                spin.SpinOnce();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Demo_1();

            Console.ReadKey();
        }

        static void Demo_1()
        {
            bool someBoolean = false;
            int numYields = 0;

            // First task: SpinWait until someBoolean is set to true
            Task t1 = Task.Factory.StartNew(() =>
            {
                SpinWait sw = new SpinWait();
                while (!someBoolean)
                {
        // The NextSpinWillYield property returns true if
        // calling sw.SpinOnce() will result in yielding the
        // processor instead of simply spinning.
        if (sw.NextSpinWillYield) numYields++;
                    sw.SpinOnce();
                }

    // As of .NET Framework 4: After some initial spinning, SpinWait.SpinOnce() will yield every time.
    Console.WriteLine("SpinWait called {0} times, yielded {1} times", sw.Count, numYields);
            });

            // Second task: Wait 100ms, then set someBoolean to true
            Task t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                someBoolean = true;
            });

            // Wait for tasks to complete
            Task.WaitAll(t1, t2);
        }

    }
}
