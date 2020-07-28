using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> method = Work;
            IAsyncResult cookie = method.BeginInvoke("test", null, null);
            //IAsyncResult cookie = method.BeginInvoke("test", Done, method);

            //
            // ... here's where we can do other work in parallel...
            //
            int result = method.EndInvoke(cookie);
            Console.WriteLine("String length is: " + result);

            Console.ReadKey();
        }

        static int Work(string s) { return s.Length; }

        static void Done(IAsyncResult cookie)
        {
            var target = (Func<string, int>)cookie.AsyncState;
            int result = target.EndInvoke(cookie);
            Console.WriteLine("String length is: " + result);
        }
    }
}
