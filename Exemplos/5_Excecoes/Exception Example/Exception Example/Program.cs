using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 3;
            int b = 0;

            Console.WriteLine("a divided by b is: " + a / b);

            //All code in the try block will be checked for exceptions
            try
            {
                Console.WriteLine("a divided by b is: " + a / b);
            }
            //If an exception occurs, the code in the catch block will be run
            catch
            {
                Console.WriteLine("An error occurred...");
            }
            finally
            {
                Console.WriteLine("Finally code has been executed...");
            }

            //try
            //{
            //    Console.WriteLine("a divided by b is: " + a / b);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The following error has occured : " + e.Message);
                Console.WriteLine("ExceptionType: {0}", e.GetType());
            //}
            //finally
            //{
            //    Console.WriteLine("The Finally section has bee run...");
            //}
        }

       
    }
}
