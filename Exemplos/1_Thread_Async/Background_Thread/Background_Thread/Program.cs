using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Background_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            // Starts a new foreground thread running the Count method
            // writes "FG" in the output
            Thread t = new Thread(Count);
            t.Start();

            // Starts a background thread that writes "BG" in the output
            Task task = Task.Run(() =>
            {
                // The task is running on a background thread and will
                // produce 8 BG's in the output
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(500);
                    sw.Stop();
                    Debug.WriteLine("BG: Elapsed {0:N2} miliseconds", sw.ElapsedMilliseconds);
                }
            });

            Console.ReadKey();
        }

        static void Count()
        {
            var sw = Stopwatch.StartNew();

            // Set this condition number higher than 8 (which is the condition in Task.Run)
            // to see that this foreground thread continues to run when the Task completes
            // Set this condition number lower than 8 to see that when this foreground thread
            // completes the background task thread ends.
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(500);
                sw.Stop();
                Debug.WriteLine("FG: Elapsed {0:N2} miliseconds", sw.ElapsedMilliseconds);
            }
        }

    }
}
