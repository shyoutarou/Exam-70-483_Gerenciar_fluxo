using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_return
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        // Calculate a number's factorial.
        private long Factorial(long n)
        {
            // Make sure n >= 0.
            if (n < 0) return 0;
            checked
            {
                try
                {
                    long result = 1;
                    for (long i = 2; i <= n; i++) result *= i;
                    return result;
                }
                catch
                {
                    return 0;
                }
            }
        }

        // Calculate a number's factorial.
        private long Factorial_Checked(long n)
        {
            // Make sure n >= 0.
            if (n < 0) throw new ArgumentOutOfRangeException(
            "n", "The number n must be at least 0 to calculate n!");
            checked
            {
                long result = 1;
                for (long i = 2; i <= n; i++) result *= i;
                return result;
            }
        }
    }
}

