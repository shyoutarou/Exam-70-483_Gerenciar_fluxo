using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicate_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("========Predicate=========");

            Predicate<string> Predicate_isUpper = IsUpperCase;

            bool result = Predicate_isUpper("hello world!!");
            Console.WriteLine("Predicate: " + result);

            result = Predicate_isUpper("HELLO!");
            Console.WriteLine("Predicate: " + result);

            Predicate<string> AnonimaPredicate = delegate (string s) { return s.Equals(s.ToUpper()); };
            Console.WriteLine("Anonima Predicate: " + AnonimaPredicate("olá mundo !!"));

            Predicate<string> LambdaPredicate = s => s.Equals(s.ToUpper());
            Console.WriteLine("Lambda Predicate: " + LambdaPredicate("olá mundo !!"));

            Console.ReadKey();

        }

        public static bool IsUpperCase(string str)
        {
            return str.Equals(str.ToUpper());
        }
    }
}
