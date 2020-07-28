using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async_Scalability
{
    class Program
    {

        public static string DebugInfo
        {
            get
            {
                ThreadPool.GetMaxThreads(out var maxThreads, out _);
                ThreadPool.GetAvailableThreads(out var threads, out _);
                var usedThreads = maxThreads - threads;
                var mt = $"{usedThreads.ToString().PadLeft(4)}/{maxThreads.ToString().PadLeft(4)}";
                return $"Threads {mt.PadRight(8)}";
            }
        }

        static void Main(string[] args)
        {
            var ms = 5000;
            Console.WriteLine("Start " + DebugInfo);
            var listA = Enumerable.Range(0, 10).Select(x => SleepAsyncA(ms));
            Task.WaitAll(listA.ToArray());

            var listB = Enumerable.Range(0, 10).Select(x => SleepAsyncB(ms));
            Task.WaitAll(listB.ToArray());

            var listC = Enumerable.Range(0, 10).Select(x => SleepAsyncC(ms));
            Task.WaitAll(listC.ToArray());

            Console.ReadKey();
        }

        public static Task SleepAsyncA(int millisecondsTimeout)
        {
            return Task.Run(() => { Console.WriteLine("SleepAsyncA " + DebugInfo); Thread.Sleep(millisecondsTimeout); });
        }

        public static Task SleepAsyncB(int millisecondsTimeout)
        {
            TaskCompletionSource<bool> tcs = null;
            var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);

            Console.WriteLine("SleepAsyncB " + DebugInfo);

            tcs = new TaskCompletionSource<bool>(t);
            t.Change(millisecondsTimeout, -1);

            return tcs.Task;
        }

        public static Task SleepAsyncC(int millisecondsTimeout)
        {
            Console.WriteLine("SleepAsyncC " + DebugInfo);
            return Task.Delay(millisecondsTimeout);
        }

    }
}
