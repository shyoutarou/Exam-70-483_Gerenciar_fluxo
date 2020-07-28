using System;
using System.Threading;

namespace ThreadStatic_Atributo
{
    class Program
    {
        [ThreadStatic]
        static int _count_ts = 0;
        static int _count = 0;

        static void Main(string[] args)
        {
            Thread threadA = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("ThreadA _count_ts = {0} ", _count_ts++);
                    Console.WriteLine("   ThreadA _count = {0} ", _count++);
                }
            });
            Thread threadB = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("ThreadB _count_ts = {0} ", _count_ts++);
                    Console.WriteLine("   ThreadB _count = {0} ", _count++);
                }
            });
            threadA.Start();
            threadB.Start();

            Console.ReadKey();
        }
    }
}
