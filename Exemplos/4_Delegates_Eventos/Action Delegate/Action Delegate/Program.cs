using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========Action============");

            Action<string, int, int> soma_action = AddNumbers;
            soma_action("Action", 100, 5);

            Action<int> AnonimaActionDel = delegate (int i)
            {
                Console.WriteLine("Action Anonima Numero: " + i);
            };
            AnonimaActionDel(10);

            Action<int> LambdaActionDel = i => Console.WriteLine("Action Lambda Numero: " + i);
            LambdaActionDel(10);

            Console.ReadKey();
        }

        public static void AddNumbers(string funcao, int a, int b)
        {
            var soma = a + b;
            Console.WriteLine(funcao + " AddNumbers: " + soma);
        }
    }
}
