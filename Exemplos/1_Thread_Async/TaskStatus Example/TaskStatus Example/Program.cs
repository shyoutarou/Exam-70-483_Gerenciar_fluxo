using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskStatus_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t3 = new Task(myMethod);
            t3.Start();

            Task<int>[] tarefas = new Task<int>[2];
            tarefas[0] = new Task<int>(() => { return 34; });

            var situacao = tarefas[0].Status;
            Console.WriteLine("Status == " + situacao); //Status == Created

            tarefas[0].Start();
            situacao = tarefas[0].Status;
            Console.WriteLine("Status == " + situacao); //Status == WaitingToRun

            var tarefa = FazerAlgo();
            Console.WriteLine("Status == " + tarefa.Status); //Status == WaitingForActivation

            var tarefaconcluida = TarefaConcluida();
            Console.WriteLine("Status == " + tarefaconcluida.Status); //Status == RanToCompletion
            tarefaconcluida.Start();

            tarefa.Start();

            Console.ReadKey();
        }

        static async Task FazerAlgo()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        static async Task TarefaConcluida()
        {
            Console.WriteLine("Tarefa realizada");
        }


        static async Task MetodoTarefa()
        {
            Console.WriteLine("Tarefa realizada");
        }

        private static void myMethod()
        {
            Console.WriteLine("Hello From My Task");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine("Bye From My Task");
        }

    }
}
