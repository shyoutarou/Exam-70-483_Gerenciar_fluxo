using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCircuit
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 42;
            int y = 1;
            int z = 42;
            Console.WriteLine(x == y); // Displays false
            Console.WriteLine(x == z); // Displays true

            bool x2 = true;
            bool y2 = false;
            bool result = x2 || y2;
            Console.WriteLine(result); // Displays True

            OrShortCircuit();


            int value = 42;
            bool result2 = (0 < value) && (value < 100);

            bool a = true;
            bool b = false;
            Console.WriteLine(a ^ a); // False
            Console.WriteLine(a ^ b); // True
            Console.WriteLine(b ^ b); // False


            Console.ReadKey();
        }

        public static void OrShortCircuit()
        {
            bool x = true;
            bool result = x || GetY();
        }
        private static bool GetY()
        {
            Console.WriteLine("This method doesn’t get called");
            return true;
        }

        public void Process(string input)
        {
            bool result = (input != null) && (input.StartsWith(“v”));
            // Do something with the result
        }
    }
}
