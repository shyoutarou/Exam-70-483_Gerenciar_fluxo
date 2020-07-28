using System;

namespace Delegate_Example
{
    class Program
    {
        delegate void del_anonimo(string str);
        delegate void del_lambda(string str);

        static void Main(string[] args)
        {
            Console.WriteLine("=========Anônimo&Lambda============");

            del_anonimo delanonimo = delegate (string name)
            { Console.WriteLine($"Delegate anonimo: {name}"); };

            del_lambda dellambda = name => { Console.WriteLine($"Delegate lambda: {name}"); };

            delanonimo("Anônimo");
            dellambda("Lambda");

            Console.ReadKey();
        }
    }
}





