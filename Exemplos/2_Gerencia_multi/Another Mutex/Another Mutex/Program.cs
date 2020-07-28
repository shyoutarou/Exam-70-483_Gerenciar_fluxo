using System;
using System.Threading;

namespace Another_Mutex
{

    class Program
    {
        static Mutex m1 = new Mutex(true, "Questpond");

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
            if (m1.WaitOne(5000, false) == false)
                return false;
            else
                return true;
        }
    }
}
