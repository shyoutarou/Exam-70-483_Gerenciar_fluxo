using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace while_loop
{
    class Program
    {
        static void Main(string[] args)
        {
            loop_through_array();
            Console.ReadLine();
        }

        static void loop_through_array()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            int index = 0;
            while (index < values.Length)
            {
                Console.Write(values[index]);
                index++;
            }

            do
            {
                Console.WriteLine("Executed once!");
            }
            while (false);


            do
            {
                Console.WriteLine(index);
                index--;
            } while (index != 0);

            // while statement example
            char someValue;
            do
            {
                someValue = (char)Console.Read();
                Console.WriteLine(someValue);
            } while (someValue != 'q');

            Console.ReadKey();
        }
    }
}
