using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lock_Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            Nonatomic_Erro();
            Atomic_Lock();
            Console.ReadKey();
        }

        static void Atomic_Lock()
        {
            int n = 0;
            object _lock = new object();
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    lock (_lock)
                        n++;
                //OU Interlocked.Increment(ref n);
            });
            for (int i = 0; i < 1000000; i++)
                lock (_lock)
                    n--;
            //OU Interlocked.Decrement(ref n);

            up.Wait();
            Console.WriteLine(n);
        }

        static void Nonatomic_Erro()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    n++;
            });
            for (int i = 0; i < 1000000; i++)
                n--;
            up.Wait();
            Console.WriteLine(n); //-205 OU o OU 181469
        }
    }
}
