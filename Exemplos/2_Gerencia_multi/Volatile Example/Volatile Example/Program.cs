using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Volatile_Example
{
    public class IfYouThinkYouUnderstandVolatile
    {
        volatile int x, y;

        public void Test1()        // Executed on one thread
        {
            x = 1;            // Volatile write (release-fence)
            int a = y;        // Volatile read (acquire-fence)
            Console.WriteLine($"Test1: a = {a} e y = {y} e x = {x}");
        }

        public void Test2()        // Executed on another thread
        {
            y = 1;            // Volatile write (release-fence)
            int b = x;        // Volatile read (acquire-fence)
            Console.WriteLine($"Test2: b = {b} e y = {y} e x = {x}");
        }
    }

    class Program
    {
        private static volatile int _flag = 0;
        //private static int _flag = 0;
        private static int _value = 0;

        static void Main(string[] args)
        {
            //] Não é possível simular erro...
            //for (int i = 0; i < 100; i++)
            //{
            //    Teste_Concorrencia();
            //}

            for (int i = 0; i < 5; i++)
            {
                Teste_Concorrencia_2();
            }

            Console.ReadKey();
        }

        public static void Thread1()
        {
            _value = 5;
            _flag = 1;
        }
        public static void Thread2()
        {
            if (_flag == 1) Console.WriteLine(_value);
        }

        static void Teste_Concorrencia()
        {
            Random rnd = new Random();

            Task t1 = Task.Run(() =>
            {
                Thread1();
                Thread.Sleep(rnd.Next(0, 1000));
            });
            Task t2 = Task.Run(() =>
            {
                Thread2();
            });
            Task.WaitAll(t1, t2);
            Console.WriteLine("FIM"); // Senão aplicar lock Displays 2, senão 3
        }

        static void Teste_Concorrencia_2()
        {
            var aclasse = new IfYouThinkYouUnderstandVolatile();
            Random rnd = new Random();

            Task t1 = Task.Run(() =>
            {
                aclasse.Test1();
                Thread.Sleep(rnd.Next(0, 1000));
            });
            Task t2 = Task.Run(() =>
            {
                aclasse.Test2();
            });
            Task.WaitAll(t1, t2);
            Console.WriteLine("FIM"); // Senão aplicar lock Displays 2, senão 3
        }
    }
}
