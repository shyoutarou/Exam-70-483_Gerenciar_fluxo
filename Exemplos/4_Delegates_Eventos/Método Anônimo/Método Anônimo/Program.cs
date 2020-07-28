using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Método_Anônimo
{
    class Program
    {
        private static Func<float, float> Function = delegate (float x) { return x * x; };

        static void Main(string[] args)
        {
            //Anonymous method that doesn't return value
            Action act = delegate ()
            {
                Console.WriteLine("Inside Anonymous method");
            };
            //Anonymous method that does return value
            Func<int, int> func = delegate (int num)
            {
                Console.Write("Inside Func: ");
                return (num * 2);
            };
            act();
            Console.WriteLine(func(4)); // Inside Func: 8


            TestAnonymous(() =>
            {
                Console.WriteLine("Pass anonymous method in method's perameter");
            });


            var resultado = Function(Convert.ToSingle(4.3));
            Console.WriteLine("Quadrado:" + resultado); // Quadrado:18,49


            Thread thread = new Thread(delegate ()
            {
                Console.WriteLine("Hello World");
            });

            Thread.Sleep(3000);
            thread.Start();


            Console.ReadKey();
        }

        public static void TestAnonymous(Action act)
        {
            act();
        }


    }
}
