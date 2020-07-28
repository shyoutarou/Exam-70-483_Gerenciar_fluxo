using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goto_keyword
{
    class Program
    {
        static void Main(string[] args)
        {

            int x = 3;
            if (x == 3)
            {
                goto customLabel;
                x++;
            }

        customLabel:
            Console.WriteLine();
            Console.WriteLine(x);


            Console.ReadLine();
        }
    }
}
