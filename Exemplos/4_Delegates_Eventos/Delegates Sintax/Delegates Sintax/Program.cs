using System;

namespace Delegates_Sintax
{
    class Program
    {
        static void Notify(string name)
        {
            Console.WriteLine($"Notification received for: {name}");
        }

        delegate void _delegado(string str);

        static void Main(string[] args)
        {
            // No C# 1.0 e versões posteriores
            _delegado del1 = new _delegado(Notify);

            //O C# 2.0 oferece uma maneira mais simples 
            _delegado del2 = Notify;

            //No C# 2.0 e versões posteriores, com método anônimo 
            _delegado del3 = delegate (string name)
            {
                Console.WriteLine($"Notification received for: {name}");
            };

            // No C# 3.0 e versões posteriores, com expressão lambda, 
            _delegado del4 = name => { Console.WriteLine($"Notification received for: {name}"); };
        }
    }
}
