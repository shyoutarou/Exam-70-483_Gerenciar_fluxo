using System;
using System.Threading.Tasks;

namespace TaskFactory_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskFactory tf = new TaskFactory();
            Task t = tf.StartNew(FazerAlgo);

            Task t1 = Task.Factory.StartNew(() => FazerAlgo());
            Task t2 = Task.Factory.StartNew(FazerAlgo);
            Console.ReadKey();
        }

        static void FazerAlgo()
        {
            Console.WriteLine("executando uma tarefa => FazerAlgo()");
        }

    }
}
