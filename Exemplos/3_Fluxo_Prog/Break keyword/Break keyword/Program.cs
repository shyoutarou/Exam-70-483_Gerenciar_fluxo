using System;

namespace Break_keyword
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i = 0; i < 10; i++)
            {
                if (numbers[i] == 3)
                {
                    break;
                }
                Console.Write(numbers[i]);
            }
            Console.WriteLine("End of Loop");
            Console.ReadLine();
        }
    }
}
