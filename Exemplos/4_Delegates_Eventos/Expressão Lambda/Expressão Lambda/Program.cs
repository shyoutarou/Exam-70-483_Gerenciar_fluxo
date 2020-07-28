using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressão_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lambda Expression that doesn't return value
            Action act = () =>
            {
                Console.WriteLine("Inside Lambda Expression");
            };
            //Lambda Expression that does have return value
            Func<int, int> func = (int num) =>
            {
                Console.Write("Inside Func: ");
                return (num * 2);
            };
            act();
            Console.WriteLine(func(4)); // Inside Func: 8

            // Implement inline anonymous method
            //Lambda Expression that doesn't return value
            Action actinline = () => Console.WriteLine("Hello World");
            //Lambda Expression that does have return value
            Func<int, int> funcinline = (int num) => num * 2;
            actinline();
            Console.WriteLine(funcinline(4)); // 8

            // Anonymous method without specifying parameter type
            //type of name will be string
            Action<string> actName = (name) => Console.WriteLine(name);
            //for single parameter, we can neglect () paranthese
            Action<string> actName2 = name => Console.WriteLine(name);
            Func<int, int> mul = x => x * 2;
            actName("Hello");
            actName2("World");
            Console.WriteLine(mul(4)); // 8


            //Pass Lambda expression as parameter
            TestLambda(() => Console.WriteLine("Inside Lambda"));


            Console.ReadKey();
        }

        static void TestLambda(Action act)
        {
            Console.WriteLine("Test Lambda Method");
            act();
        }
    }
}
