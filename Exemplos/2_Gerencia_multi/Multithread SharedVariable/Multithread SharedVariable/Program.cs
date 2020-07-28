using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithread_SharedVariable
{
    class Program
    {
        static void Main(string[] args)
        {
            Exemplo_Erro();
            Exemplo_Erro2();

            Console.ReadKey();
        }

        static void Exemplo_Erro()
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

        static void Exemplo_Erro2()
        {
            int num = 0;
            int length = 500000;
            //Run on separate thread of threadpool
            Task tsk = Task.Run(() =>
            {
                for (int i = 0; i < length; i++)
                {
                    num = num + 1;
                }
            });
            //Run on Main Thread
            for (int i = 0; i < length; i++)
            {
                num = num - 1;
            }
            tsk.Wait();
            Console.WriteLine(num); //-6674 OU o OU 655
        }
    }
}
