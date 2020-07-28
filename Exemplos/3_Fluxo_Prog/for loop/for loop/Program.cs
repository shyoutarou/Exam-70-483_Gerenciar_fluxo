using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace for_loop
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
            for (int index = 0; index < values.Length; index++)
            {
                Console.Write(values[index]);
            }


            for (int x = 0, y = values.Length - 1;
            ((x < values.Length) && (y >= 0));
            x++, y--)
            {
                Console.Write(values[x]);
                Console.Write(values[y]);
            }

            for (int index = 0; index < values.Length; index += 2)
            {
                Console.Write(values[index]);
            }


            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) break;
                Console.Write(values[index]);
            }

            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) continue;
                Console.Write(values[index]);
            }

        }

        static void loop_empty_infinity()
        {
            // empty for loop
            for (int counter = 0; counter >= 10; counter++)
            {
                ;
            }

            // infinite for loop in C#
            for (; ; )
            {
                ;
            }
        }

    }
}
