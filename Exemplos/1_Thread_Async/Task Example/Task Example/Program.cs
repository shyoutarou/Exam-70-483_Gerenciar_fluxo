using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando uma Thread com delay de 5 s");
            //Threads
            Thread th = new Thread(() =>
            {
                Thread.Sleep(5000);
            });
            Console.WriteLine("Iniciando uma Task com delay de 5 s");
            //Tasks
            Task t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
            });
            Console.ReadLine();
        }
    }
}
