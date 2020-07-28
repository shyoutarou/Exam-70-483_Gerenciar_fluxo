using System;
using System.Collections.Generic;
using System.Threading;

namespace ReaderWriterLockSlim_Ex
{
    class Program
    {
        static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        static List<int> items = new List<int>();
        static Random rand = new Random();
        static void Main(string[] args)
        {
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Write).Start("A");
            new Thread(Write).Start("B");
            Console.Read();
        }

        static void Read()
        {
            while (true)
            {
                rw.EnterReadLock();
                foreach (int i in items) Thread.Sleep(10);
                rw.ExitReadLock();
            }
        }
        static void Write(object threadID)
        {
            while (true)
            {
                int newNumber = GetRandNum(50);
                rw.EnterWriteLock();
                items.Add(newNumber);
                rw.ExitWriteLock();
                Console.WriteLine("Thread " + threadID + " added " + newNumber);
                Thread.Sleep(100);
            }
        }
        static int GetRandNum(int max)
        {
            lock (rand)
                return rand.Next(max);
        }
    }
}
