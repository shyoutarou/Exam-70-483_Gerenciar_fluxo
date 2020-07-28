using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invocar_Delegate
{
    class Program
    {
        delegate void del_invoca(string str);
        public delegate int del_Calculate(int x, int y);

        static void Main(string[] args)
        {
            // Instantiate the delegate.
            del_invoca handler = DelegateMethod;

            // Call the delegate.
            handler("Hello World");


            //O delegado também pode ser chamado usando o método.invoke.
            handler.Invoke("Ali Asad");

            List<del_Calculate> function = new List<del_Calculate>();

            Console.ReadKey();
        }

        // Create a method for a delegate.
        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }

        public int Add(int x, int y) { return x + y; }
        public int Multiply(int x, int y) { return x * y; }
        public void UseDelegate()
        {
            del_Calculate calc = Add;
            Console.WriteLine(calc(3, 4)); // Displays 7
            calc = Multiply;
            Console.WriteLine(calc(3, 4)); // Displays 12
        }
    }
}
