using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore_Example
{
    class Program
    {
        static Semaphore s1 = new Semaphore(2, 2, "SemaphoreQuestpond");

        static void Main(string[] args)
        {

            if (IsInstance() == true)
            {
                Console.WriteLine("New Instance created...");
            }
            else
            {
                Console.WriteLine("Instance already acquired...");
            }

            Console.ReadLine();
        }

        static bool IsInstance()
        {

            if (s1.WaitOne(5000, false) == false)
                return false;
            else
                return true;
        }
    }
}
