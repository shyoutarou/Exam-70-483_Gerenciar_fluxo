using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========Func============");

            Func<int, int, int> subtrai_func = SubtraiNumbers;

            var resultado = subtrai_func(10, 5);
            Console.WriteLine("Func SubtraiNumbers: " + resultado);

            Func<int> getRandomNumber = delegate ()
            {
                Random rnd = new Random();

                var numero = rnd.Next(1, 100);

                return numero;
            };


            Console.WriteLine("Func Anonima Random de 100: " + getRandomNumber());

            getRandomNumber = () => new Random().Next(1, 100);
            Func<int, int, int> Sum = (x, y) => x + y;

            Console.WriteLine("Func Lambda Random de 100: " + getRandomNumber());
            Console.WriteLine("Func Lambda Soma: " + Sum(5, 300));

            Console.ReadKey();
        }

        public static int SubtraiNumbers(int a, int b)
        {
            var subtrai = a - b;
            return subtrai;
        }
    }
}
