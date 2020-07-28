using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowException_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                long n;
                string nTextBox = Int64.MaxValue.ToString();
                if (!long.TryParse(nTextBox, out n))
                {
                    Console.WriteLine("The number must be an integer.");
                    return;
                }
                var resultTextBox = Factorial(n).ToString();

                Console.WriteLine("Resultado: " + resultTextBox);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The number must be at least 0.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("This number is too big to calculate its factorial.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Calculate a number's factorial.
        private static long Factorial(long n)
        {
            // Make sure n >= 0.
            if (n < 0) throw new ArgumentOutOfRangeException(
            "n", "The number n must be at least 0 to calculate n!");

            long result = 1;
            for (long i = 2; i <= n; i++) result *= i;
            return result;



            //checked
            //{
            //    long result = 1;
            //    for (long i = 2; i <= n; i++) result *= i;
            //    return result;
            //}
        }
    }
}
