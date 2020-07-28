using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace switch_statement
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            Check(input[0]);
            CheckWithSwitch(input[0]);
            Switch_Goto();

            Console.ReadLine();
        }


        static void Check(char input)
        {
            if (input == 'a' || input == 'e' || input == 'i'
                             || input == 'o' || input == 'u')
                Console.WriteLine("Input is a vowel");
            else
                Console.WriteLine("Input is a consonant");
        }


        static void CheckWithSwitch(char input)
        {
            switch (input)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    {
                        Console.WriteLine("Input is a vowel");
                        break;
                    }
                case 'y':
                    {
                        Console.WriteLine("Input is sometimes a vowel.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input is a consonant");
                        break;
                    }
            }
        }

        static void Switch_Goto()
        {
            int i = 1;
            switch (i)
            {
                case 1:
                    {
                        Console.WriteLine("Case 1");
                        goto case 2;
                    }
                case 2:
                    {
                        Console.WriteLine("Case 2");
                        break;
                    }
            }
        }
    }
}
