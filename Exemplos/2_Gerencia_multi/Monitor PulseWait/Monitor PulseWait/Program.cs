using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_PulseWait
{
    class BlockingQueue<T>
    {
        readonly int _Size = 0;
        readonly Queue<T> _Queue = new Queue<T>();
        readonly object _Key = new object();
        bool _Quit = false;

        public BlockingQueue(int size)
        {
            _Size = size;
        }

        public void Quit()
        {
            lock (_Key)
            {
                _Quit = true;
                Monitor.PulseAll(_Key);
            }
        }

        public bool Enqueue(T t)
        {
            lock (_Key)
            {
                while (!_Quit && _Queue.Count >= _Size) Monitor.Wait(_Key);

                if (_Quit) return false;

                _Queue.Enqueue(t);
                Monitor.PulseAll(_Key);
            }
            return true;
        }

        public bool Dequeue(out T t)
        {
            t = default(T);

            lock (_Key)
            {
                while (!_Quit && _Queue.Count == 0) Monitor.Wait(_Key);

                if (_Queue.Count == 0) return false;

                t = _Queue.Dequeue();
                Monitor.PulseAll(_Key);
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            object key = new object();

            // thread A
            lock (key) Monitor.Wait(key);

            // thread B
            lock (key) Monitor.Pulse(key);

            bool block = true;

            // thread A
            lock (key)
            {
                while (block)
                    Monitor.Wait(key);
                block = true;
            }

            // thread B
            lock (key)
            {
                block = false;
                Monitor.Pulse(key);
            }

            Test();
            Console.ReadKey();
        }

        internal static void Test()
        {
            var q = new BlockingQueue<int>(4);

            // Producer
            new Thread(() =>
            {
                for (int x = 0; ; x++)
                {
                    if (!q.Enqueue(x)) break;
                    Trace.WriteLine(x.ToString("0000") + " >");
                }
                Trace.WriteLine("Producer quitting");
            }).Start();

            // Consumers
            for (int i = 0; i < 2; i++)
            {
                new Thread(() =>
                {
                    for (; ; )
                    {
                        Thread.Sleep(100);
                        int x = 0;
                        if (!q.Dequeue(out x)) break;
                        Trace.WriteLine("     < " + x.ToString("0000"));
                    }
                    Trace.WriteLine("Consumer quitting");
                }).Start();
            }

            Thread.Sleep(1000);

            Trace.WriteLine("Quitting");

            q.Quit();
        }
    }
}
