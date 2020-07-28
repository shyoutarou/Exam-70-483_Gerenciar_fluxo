using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checked__unchecked
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //checkedExample();

                //uncheckedExample();

                flutuanteExample();
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

            Console.ReadKey();
        }

        static void checkedExample()
        {
            //
            // Use checked overflow context.
            //
            checked
            {
                // Increment up to max.
                int num = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    num++;
                }
                // Increment up to max again (error).
                for (int i = 0; i < int.MaxValue; i++)
                {
                    num++;
                }
            }
        }

        static void uncheckedExample()
        {
            // The first short will overflow after the second short does.
            short estoura = 0;
            short naoestoura = 100;
            try
            {
                //
                // Keep incrementing the shorts until an exception is thrown.
                // ... This is terrible program design.
                //
                while (true)
                {
                    checked
                    {
                        estoura++;
                    }
                    unchecked
                    {
                        naoestoura++;
                    }
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("TOverflowException em estoura++.");
                // Display the value of the shorts when overflow exception occurs.
                Console.WriteLine(estoura);  //  32767, valor mácimo de um short
                Console.WriteLine(naoestoura);  // -32669
            }
        }


        static void flutuanteExample()
        {
            var zero = 0.0f;
            // This will return true.
            if (Single.IsNaN(0 / zero))
            {
                Console.WriteLine("Single.IsNan() can determine whether a value is not-a-number.");
            }


            // This will return "true".
            Console.WriteLine("IsPositiveInfinity(4.0 / 0) == {0}.", Double.IsPositiveInfinity(4.0 / 0) ? "true" : "false");

            // This will equal Infinity.
            Console.WriteLine("PositiveInfinity plus 10.0 equals {0}.", (Single.PositiveInfinity + 10.0).ToString());

            // This will return true.
            Console.WriteLine("IsNegativeInfinity(-5.0F / 0) == {0}.", Single.IsNegativeInfinity(-5.0F / 0) ? "true" : "false");


            // This will return "true".
            Console.WriteLine("IsNegativeInfinity(-5.0 / 0) == {0}.", Double.IsNegativeInfinity(-5.0 / 0) ? "true" : "false");

            if ((Double.MaxValue + 10) > Double.MaxValue)
                Console.WriteLine("Your number is bigger than a double.");

            // This will equal Infinity.
            Console.WriteLine("10.0 minus NegativeInfinity equals {0}.", (10.0 - Double.NegativeInfinity).ToString());
        }
    }
}
