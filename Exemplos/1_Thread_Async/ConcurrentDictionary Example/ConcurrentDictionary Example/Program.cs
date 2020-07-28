using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictionary_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenericCollection_MultipleThreads();
            ConcurrentDictionary_MultipleThreads();


            var dict = new ConcurrentDictionary<string, int>();
            if (dict.TryAdd("k1", 42)) Console.WriteLine("Added");

            if (dict.TryUpdate("k1", 21, 42)) Console.WriteLine("42 updated to 21");

            dict["k1"] = 42; // Overwrite unconditionally
            int r1 = dict.AddOrUpdate("k1", 3, (s, i) => i * 2);
            int r2 = dict.GetOrAdd("k2", 3);


            Console.ReadKey();
        }


        static void GenericCollection_MultipleThreads()
        {
            try
            {
                Dictionary<int, int> dic = new Dictionary<int, int>();
                Task tsk1 = Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                        dic.Add(i, i + 1);
                });
                Task tsk2 = Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                        dic.Add(i + 1, i);
                });
                Task[] allTasks = { tsk1, tsk2 };
                Task.WaitAll(allTasks); // Wait for all tasks
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ConcurrentDictionary_MultipleThreads()
        {
            ConcurrentDictionary<int, int> dic = new ConcurrentDictionary<int, int>();
            Task tsk1 = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (dic.TryAdd(i, i + 1)) Console.WriteLine($"tsk1 Key {i} Added {i + 1}");
                    if (dic.TryUpdate(5, 442, 6)) Console.WriteLine("6 updated to 442");
                }
            });
            Task tsk2 = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (dic.TryAdd(i + 1, i)) Console.WriteLine($"tsk2 Key {i + 1}  Added {i}");
                }

                dic[50] = 42; // Overwrite unconditionally
                Console.WriteLine($"tsk2 Key {50}  Value {dic[50]}");
                int r1 = dic.AddOrUpdate(50, 3, (s, x) => x * 2);
                Console.WriteLine($"tsk2 Key {50}  Value {dic[50]}");
                int r2 = dic.GetOrAdd(201, 888);
                Console.WriteLine($"tsk2 Key {201}  Value {r2}");
            });
            Task[] allTasks = { tsk1, tsk2 };
            Task.WaitAll(allTasks); // Wait for all tasks

            Console.WriteLine("Program ran succussfully");
        }
    }
}
