using System;

namespace beyond_basic_if_statements
{
    class Program
    {
        static void Main(string[] args)
        {
            bool condition1;
            bool condition2;
            bool condition3;
            // single if statement
            condition1 = true;
            if (condition1)
            {
                Console.WriteLine("This statement prints if condition is true");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
            //nested if statement
            condition1 = true;
            condition2 = true;
            if (condition1)
            {
                if (condition2)
                {
                    Console.WriteLine("This only prints if both conditions are true.");
                }
            }
            Console.WriteLine();
            // if statement with logical operator
            condition1 = true;
            condition2 = true;
            if (condition1 && condition2)
            {
                Console.WriteLine("This only prints if both conditions are true.");
            }
            Console.WriteLine();
            // if-else statement
            condition1 = true;
            if (condition1)
            {
                Console.WriteLine("This statement prints if condition is true.");
            }
            else
            {
                Console.WriteLine("This statement prints if condition is false.");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
            // if-else if statement
            condition1 = true;
            condition2 = false;
            condition3 = false;
            if (condition1)
            {
                Console.WriteLine("This statement prints if condition1 is true.");
            }
            else if (condition2)
            {
                Console.WriteLine("This statement prints if condition2 is true.");
            }
            else if (condition3)
            {
                Console.WriteLine("This statement prints if condition3 is true.");
            }
            else
            {
                Console.WriteLine("This statement prints if previous conditions are false.");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
