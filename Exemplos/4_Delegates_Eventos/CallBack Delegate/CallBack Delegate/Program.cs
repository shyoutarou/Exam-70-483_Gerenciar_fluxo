using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallBack_Delegate
{
    class Program
    {
        public delegate void del_invoca(string message);

        static void Main(string[] args)
        {
            del_invoca handler = DelegateMethod;
            MethodWithCallback(1, 2, handler); //The number is: 3

            Console.ReadKey();
        }

        // Create a method for a delegate. 
        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }

        static public void MethodWithCallback(int param1, int param2, del_invoca callback)
        {
            callback("The number is: " + (param1 + param2).ToString());
        }
    }
}
