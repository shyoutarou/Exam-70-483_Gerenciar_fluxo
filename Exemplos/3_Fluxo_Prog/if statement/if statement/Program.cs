using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_statement
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a = true;
            if (a) Console.WriteLine(a);

            bool b = true;
            if (b)
            {
                Console.WriteLine("Both these lines");
                Console.WriteLine("Will be executed");
            }

            if (b)
            {
                int r = 42;
                b = false;
            }

            bool c = true;
            if (b)
                b = true;
            else if (c)
                c = true;
            else
                b = c = false;

            int? x = null;
            int? z = null;
            int y = x ?? z ?? -1;

            int? valor = null;
            if (true)
                valor = 1;
            else
                valor = 0;
            valor = p ? 1 : 0;

        }
    }
}
