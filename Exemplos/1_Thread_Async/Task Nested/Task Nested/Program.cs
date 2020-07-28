using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Nested
{
    class Program
    {
        static void Main(string[] args)
        {
            Tarefa_Detached_Child();
            Tarefa_AttachedToParent();

            Console.ReadKey();
        }

        static void Tarefa_Detached_Child()
        {
            Task outer = Task.Run(() =>
            {
                Console.WriteLine("Hi I'm outer task ");
                Task inner = Task.Run(() =>
                {
                    Console.WriteLine("Hi I'm inner task");
                    Thread.Sleep(2000);
                    Console.WriteLine("By from inner task");
                });
                Thread.Sleep(500);
                Console.WriteLine("By from outer task");
            });
            outer.Wait();
        }

        static void Tarefa_AttachedToParent()
        {
Task outer = new Task(() =>
{
    Console.WriteLine("ToParent: Hi I'm outer task ");
    //AttachedToParent only available with new Task()
    Task inner = new Task(() =>
    {
        Console.WriteLine("ToParent: HI I'm inner task");
        Thread.Sleep(2000);
        Console.WriteLine("ToParent: By from inner task");
    }, TaskCreationOptions.AttachedToParent);

    inner.Start();
    Thread.Sleep(500);
    Console.WriteLine("ToParent: By from outer task");
});
outer.Start();
outer.Wait();
        }
    }
}
